using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StockMarket.Data.Common;
using StockMarket.Model.CompanyModel;

namespace StockMarket.Data.CompanyData
{
    /// <summary>
    /// service class for <see cref="ICommonDataContext"/>
    /// </summary>
    public class CompanyDataContext : ICompanyDataContext
    {
        /// <summary>
        /// constructor for CompanyDataContext
        /// </summary>
        /// <param name="settings"></param>
        public CompanyDataContext(IDatabaseSettings settings)
        {           
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            CompanyDetails = database.GetCollection<Company>(settings.CompanyCollectionName);
            CompanyDataContextSeed.CompanysSeedData(CompanyDetails);
        }

        /// <inheritdoc/> 
        public IMongoCollection<Company> CompanyDetails { get; }
    }
}
