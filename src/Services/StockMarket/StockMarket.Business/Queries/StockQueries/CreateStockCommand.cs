using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StockMarket.Model.CompanyModel;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class CreateStockCommand: IRequest<string>
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string CompanyCode { get; set; }
        public double StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
