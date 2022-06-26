using StockMarket.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Data
{
    public class StockDataContextSeed
    {       
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