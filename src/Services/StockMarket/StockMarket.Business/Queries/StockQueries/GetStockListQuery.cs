using MediatR;
using StockMarket.Business.Queries.StockQueries;
using System;

namespace StockMarket.Business.Queries.StockQueries
{
    public class GetStockListQuery : IRequest<StockDetailsvm>
    {
        public string CompanyCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public GetStockListQuery(string companyCode, string startDate, string endDate)
        {
            CompanyCode = companyCode ?? throw new ArgumentNullException(nameof(companyCode));
            StartDate = startDate ?? throw new ArgumentNullException(nameof(startDate));
            EndDate = endDate ?? throw new ArgumentNullException(nameof(endDate));
        }
    }
}
