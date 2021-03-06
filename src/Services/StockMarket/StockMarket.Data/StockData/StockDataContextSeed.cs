using MongoDB.Driver;
using StockMarket.Model.StockModel;
using System.Linq;

namespace StockMarket.Data.StockData
{
    /// <summary>
    /// class for stock data context
    /// </summary>
    public class StockDataContextSeed
    {
        /// <summary>
        /// method for fetching stock details
        /// </summary>
        /// <param name="stockDetailsCollection"></param>
        public static void StockDetailsSeedData(IMongoCollection<Stock> stockDetailsCollection)
        {
            stockDetailsCollection.Find(s => true).Any();
            //bool existStockDetails = stockDetailsCollection.Find(s => true).Any();
            //if (!existStockDetails)
            //{
            //    stockDetailsCollection.InsertManyAsync(GetPreconfiguredStockDetails());
            //}
        }
        //private static IEnumerable<Stock> GetPreconfiguredStockDetails()
        //{
        //    return new List<Stock>()
        //     {
        //        new Stock()
        //        { 
        //            CompanyCode="C001",
        //            StockPrice=15.67,
        //             CreatedDate=DateTime.Now
        //        },
        //        new Stock()
        //        {
        //            CompanyCode="C001",
        //            StockPrice=76.81,
        //             CreatedDate=DateTime.Now
        //        },
        //        new Stock()
        //        {
        //            CompanyCode="C001",
        //            StockPrice=19.44,
        //             CreatedDate=DateTime.Now
        //        },
        //        new Stock()
        //        {
        //            CompanyCode="C001",
        //            StockPrice=20.20,
        //             CreatedDate=DateTime.Now
        //        },
        //        new Stock()
        //        {
        //            CompanyCode="C003",
        //            StockPrice=45.89,
        //             CreatedDate=DateTime.Now
        //        },
        //        new Stock()
        //        {
        //            CompanyCode="C003",
        //            StockPrice=07.89,
        //             CreatedDate=DateTime.Now
        //        }
        //    };
        //}
    }
}