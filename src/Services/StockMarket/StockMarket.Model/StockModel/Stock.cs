using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StockMarket.Model.StockModel
{
    /// <summary>
    /// class for declaring stock details
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// specifies object id code
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// specifies company code
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// specifies stock price
        /// </summary>
        public double StockPrice { get; set; }

        /// <summary>
        /// specifies created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// specifies created by
        /// </summary>
        public string CreatedBy { get; set; }
        
    }
}
