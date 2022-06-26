using StockMarket.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Data
{
    public class StockDataContext : IStockDataContext
    {
        public StockDataContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDatabaseConnectionsettings2:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDatabaseConnectionsettings2:DatabaseName"));
            StockDetails = database.GetCollection<Stock>(configuration.GetValue<string>("MongoDatabaseConnectionsettings2:CollectionName"));
            StockDataContextSeed.StockDetailsSeedData(StockDetails);
        }
       public IMongoCollection<Stock> StockDetails { get; }
    }
}
