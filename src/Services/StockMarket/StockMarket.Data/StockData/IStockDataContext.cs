using MongoDB.Driver;
using StockMarket.Model.StockModel;

namespace StockMarket.Data.StockData
{
    /// <summary>
    /// interface calss for Stock data layer
    /// </summary>
    public interface IStockDataContext
    {
        /// <summary>
        /// method for getting stock details
        /// </summary>
        IMongoCollection<Stock> StockDetails { get; }
    }
}
