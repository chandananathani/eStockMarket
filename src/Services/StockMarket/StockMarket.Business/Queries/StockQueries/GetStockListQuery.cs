using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Model.CompanyModel;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class GetStockListQuery:IRequest<StockDetailsvm>
    {
        public string CompanyCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public GetStockListQuery(string companyCode,string startDate, string endDate)
        {
            CompanyCode = companyCode ?? throw new ArgumentNullException(nameof(companyCode));
            StartDate = startDate ?? throw new ArgumentNullException(nameof(startDate));
            EndDate = endDate ?? throw new ArgumentNullException(nameof(endDate));
        }
    }
}
