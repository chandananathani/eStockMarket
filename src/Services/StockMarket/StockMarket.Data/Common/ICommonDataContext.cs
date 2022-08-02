using MongoDB.Driver;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;

namespace StockMarket.Data.Common
{
    //interface class for common data layer
    public interface ICommonDataContext
    {
        /// <summary>
        /// method for fetching userdetails
        /// </summary>
        IMongoCollection<User> UserDetails { get; }
    }
}
