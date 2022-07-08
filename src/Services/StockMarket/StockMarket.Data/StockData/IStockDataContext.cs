using MongoDB.Driver;
using StockMarket.Model.StockModel;

namespace StockMarket.Data.StockData
{
    public interface IStockDataContext
    {
         IMongoCollection<Stock> StockDetails { get; }
    }
}
