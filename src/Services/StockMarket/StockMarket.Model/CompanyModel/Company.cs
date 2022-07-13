using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StockMarket.Model.CompanyModel
{
    public class Company
    {
        [BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public string CompanyWebsite { get; set; }
        public int CompanyTurnover { get; set; }
        public string StockExchange { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }
}
