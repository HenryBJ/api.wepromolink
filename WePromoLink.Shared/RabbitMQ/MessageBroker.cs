using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WePromoLink.Shared.RabbitMQ;

public class MessageBrokerOptions
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public ushort Prefetch { get; set; } = 1;
}

public class MessageBroker<T> : IDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;
    private readonly string queueName;

    public MessageBroker(MessageBrokerOptions options)
    {
        queueName = typeof(T).Name.ToLower();
        _factory = new ConnectionFactory()
        {
            HostName = options.HostName,
            UserName = options.UserName,
            Password = options.Password,
            VirtualHost = "/",
            Port = 5672
        };

        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.BasicQos(0, options.Prefetch, false);
        _channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
    }

    public void Send(T model)
    {
        var serializedJson = JsonConvert.SerializeObject(model);
        var body = Encoding.UTF8.GetBytes(serializedJson);

        _channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

    }

    public async Task Receive(Func<T, bool> processMessage)
    {
        await Task.Yield();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var obj = JsonConvert.DeserializeObject<T>(message);

            var shouldAck = processMessage!.Invoke(obj);

            if (shouldAck)
            {
                _channel.BasicAck(ea.DeliveryTag, multiple: false); // Envía el ACK afirmativo
            }
            else
            {
                _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true); // Rechaza el mensaje y lo reencola
            }
        };

        _channel.BasicConsume(queue: typeof(T).Name.ToLower(), autoAck: false, consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();

        _channel?.Dispose();
        _connection?.Dispose();
    }

}