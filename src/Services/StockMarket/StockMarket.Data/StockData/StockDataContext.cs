using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StockMarket.Data.Common;
using StockMarket.Model.StockModel;

namespace StockMarket.Data.StockData
{
    /// <summary>
    /// service class for <see cref="IStockDataContext"/>
    /// </summary>
    public class StockDataContext : IStockDataContext
    {
        /// <summary>
        /// constructor for StockDataContext
        /// </summary>
        /// <param name="settings"></param>
        public StockDataContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            StockDetails = database.GetCollection<Stock>(settings.StockCollectionName);
            StockDataContextSeed.StockDetailsSeedData(StockDetails);
        }

        /// <inheritdoc/> 
        public IMongoCollection<Stock> StockDetails { get; }
    }
}
