using MongoDB.Driver;
using StockMarket.Model.CompanyModel;

namespace StockMarket.Data.CompanyData
{
    public interface ICompanyDataContext
    {
        IMongoCollection<Company> CompanyDetails { get; }
    }
}
