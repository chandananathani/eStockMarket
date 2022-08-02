using MongoDB.Driver;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket.Data.Common
{
    /// <summary>
    /// class for common data context
    /// </summary>
    public class CommonDataContextSeed
    {
        /// <summary>
        /// method for fetching user data
        /// </summary>
        /// <param name="userCollection"></param>
        public static void CommonSeedData(IMongoCollection<User> userCollection)
        {
            userCollection.Find(c => true).Any();           
        }    

    }
}