using MongoDB.Driver;
using StockMarket.Model.CompanyModel;

namespace StockMarket.Data.CompanyData
{
    /// <summary>
    /// interface calss for Company data layer
    /// </summary>
    public interface ICompanyDataContext
    {
        /// <summary>
        /// method for getting company details
        /// </summary>
        IMongoCollection<Company> CompanyDetails { get; }
    }
}
