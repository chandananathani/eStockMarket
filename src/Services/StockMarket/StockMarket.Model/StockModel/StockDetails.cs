using System.Collections.Generic;

namespace StockMarket.Model.StockModel
{
    /// <summary>
    /// class for declaring multiple stock lists
    /// </summary>
    public class StockDetails
    {
        /// <summary>
        /// specifies stock details
        /// </summary>
        public IList<Stock> StockList { get; set; }

        /// <summary>
        /// specifies min stock price
        /// </summary>
        public double MinStockPrice { get; set; }

        /// <summary>
        /// specifies max stock price
        /// </summary>
        public double MaxStockPrice { get; set; }

        /// <summary>
        /// specifies avg stock price
        /// </summary>
        public double AvgStockPrice { get; set; }
    }
}
