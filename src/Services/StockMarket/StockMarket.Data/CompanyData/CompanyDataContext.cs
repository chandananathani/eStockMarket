using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StockMarket.Data.Common;
using StockMarket.Model.CompanyModel;

namespace StockMarket.Data.CompanyData
{
    public class CompanyDataContext : ICompanyDataContext
    {
        public CompanyDataContext(IDatabaseSettings settings)
        {           
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            CompanyDetails = database.GetCollection<Company>(settings.CompanyCollectionName);
            CompanyDataContextSeed.CompanysSeedData(CompanyDetails);
        }
        public IMongoCollection<Company> CompanyDetails { get; }
    }
}
