using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StockMarket.Model.StockModel
{
    public class Stock
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CompanyCode { get; set; }
        public double StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
