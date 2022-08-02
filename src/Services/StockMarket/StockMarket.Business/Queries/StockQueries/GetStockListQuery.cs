using MediatR;
using StockMarket.Business.Queries.StockQueries;
using System;

namespace StockMarket.Business.Queries.StockQueries
{
    /// <summary>
    /// class is for get stock list query
    /// </summary>
    public class GetStockListQuery : IRequest<StockDetailsvm>
    {
        public string CompanyCode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        /// <summary>
        /// parameter constructor for GetStockListQuery
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public GetStockListQuery(string companyCode, string startDate, string endDate)
        {
            CompanyCode = companyCode ?? throw new ArgumentNullException(nameof(companyCode));
            StartDate = startDate ?? throw new ArgumentNullException(nameof(startDate));
            EndDate = endDate ?? throw new ArgumentNullException(nameof(endDate));
        }
    }
}
