using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace StockMarket.Business.Queries.CompanyQueries
{
    /// <summary>
    /// class for create stock command
    /// </summary>
    public class CreateStockCommand : IRequest<string>
    {
        /// <summary>
        /// specifies object id
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
