using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StockMarket.Data.Common;
using StockMarket.Model.StockModel;

namespace StockMarket.Data.StockData
{
    public class StockDataContext : IStockDataContext
    {
       public StockDataContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            StockDetails = database.GetCollection<Stock>(settings.StockCollectionName);
            StockDataContextSeed.StockDetailsSeedData(StockDetails);
        }
        public IMongoCollection<Stock> StockDetails { get; }
    }
}
