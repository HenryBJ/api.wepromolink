namespace WePromoLink.StatsWorker.Services;

using Microsoft.EntityFrameworkCore.Storage;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using WePromoLink.DTO.Statistics;

public class ChartDataRepository<TLabel, TValue>
{
    private const string DATABASE = "wepromolink";
    private readonly IMongoCollection<ChartData<TLabel, TValue>> _collection;
    private readonly IMongoClient _client;

    public ChartDataRepository(string collectionName, IMongoClient client)
    {
        _client = client;
        var database = _client.GetDatabase(DATABASE);
        _collection = database.GetCollection<ChartData<TLabel, TValue>>(collectionName);

    }

    protected void InsertChartData(ChartData<TLabel, TValue> chartData)
    {
        _collection.InsertOne(chartData);
    }

    protected async Task UpdateChartData(string id, Func<ChartData<TLabel, TValue>, ChartData<TLabel, TValue>> updateFunc)
    {
        var filter = Builders<ChartData<TLabel, TValue>>.Filter.Eq("_id", id);
        var chartData = await _collection.Find(filter).FirstOrDefaultAsync();

        if (chartData != null)
        {
            var updatedChartData = updateFunc(chartData);
            var updateDefinition = Builders<ChartData<TLabel, TValue>>.Update.Set(c => c.labels, updatedChartData.labels)
                .Set(c => c.datasets, updatedChartData.datasets);
            await _collection.UpdateOneAsync(filter, updateDefinition);
        }
        else
        {
            throw new Exception("ChartData not found.");
        }
    }

    protected bool Exists(string id)
    {
        var filter = Builders<ChartData<TLabel, TValue>>.Filter.Eq("_id", id);
        var count = _collection.Find(filter).CountDocuments();
        return count > 0;
    }

    protected ChartData<TLabel, TValue> GetChartDataById(Guid id)
    {
        // Busca el objeto chartData por su ID GUID
        var filter = Builders<ChartData<TLabel, TValue>>.Filter.Eq("_id", id);
        return _collection.Find(filter).FirstOrDefault();
    }
}
