using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System.Collections.Generic;

namespace StockMarket.Data.Common
{
    public class CommonDataContext : ICommonDataContext
    {
        public CommonDataContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            UserDetails = database.GetCollection<User>(settings.UserCollectionName);
            var collections = new MongoDB.Driver.MongoClient().GetDatabase(settings.DatabaseName)
                           .ListCollectionNames()
                           .ToListAsync();
            CommonDataContextSeed.CommonSeedData(UserDetails);
        }
        public IMongoCollection<User> UserDetails { get; }
    }
}
