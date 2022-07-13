using MongoDB.Driver;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket.Data.Common
{
    public class CommonDataContextSeed
    {
        public static void CommonSeedData(IMongoCollection<User> userCollection)
        {
            userCollection.Find(c => true).Any();           
        }    

    }
}