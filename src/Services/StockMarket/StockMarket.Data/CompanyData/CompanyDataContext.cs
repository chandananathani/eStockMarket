using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Data.CompanyData
{
    public class CompanyDataContext : ICompanyDataContext
    {
        public CompanyDataContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDatabaseConnectionsettings1:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("MongoDatabaseConnectionsettings1:DatabaseName"));
            CompanyDetails = database.GetCollection<Company>(configuration.GetValue<string>("MongoDatabaseConnectionsettings1:CollectionName"));
            CompanyDataContextSeed.CompanysSeedData(CompanyDetails);
        }
        public IMongoCollection<Company> CompanyDetails { get; }        
    }
}
