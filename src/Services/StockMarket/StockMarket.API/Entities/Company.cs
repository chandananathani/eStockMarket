using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Entities
{
    public class Company
    {
        [BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CompanyCode { get; set; }        
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyTurnover { get; set; }
        public string StockExchange { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
