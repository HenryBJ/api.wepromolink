var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// services.AddSingleton<MessageBroker<BaseEvent>>(sp =>
//         {
//             return new MessageBroker<BaseEvent>(new MessageBrokerOptions
//             {
//                 HostName = configuration["RabbitMQ:hostname"],
//                 UserName = configuration["RabbitMQ:username"],
//                 Password = configuration["RabbitMQ:password"]
//             });
//         });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
