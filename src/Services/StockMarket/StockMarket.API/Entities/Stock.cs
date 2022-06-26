using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Entities
{
    public class Stock
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CompanyCode { get; set; }
        public double StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        //public string MinStockPrice { get; set; }
        //public string MaxStockPrice { get; set; }
        //public string AvgStockPrice { get; set; }
    }
}
