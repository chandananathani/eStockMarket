using MongoDB.Driver;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;

namespace StockMarket.Data.Common
{
    public interface ICommonDataContext
    {
        IMongoCollection<User> UserDetails { get; }
    }
}
