using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class CreateStockCommand : IRequest<string>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CompanyCode { get; set; }
        public double StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
