using StockMarket.Model.StockModel;
using System.Collections.Generic;

namespace StockMarket.Business.Queries.StockQueries
{
    /// <summary>
    /// class for stock details
    /// </summary>
    public class StockDetailsvm
    {
        /// <summary>
        /// specifies the stock list
        /// </summary>
        public IList<Stock> StockList { get; set; }

        /// <summary>
        /// specifies the min stock price
        /// </summary>
        public double MinStockPrice { get; set; }

        /// <summary>
        /// specifies the max stock price
        /// </summary>
        public double MaxStockPrice { get; set; }

        /// <summary>
        /// specifies the avg stock price
        /// </summary>
        public double AvgStockPrice { get; set; }
    }
}
