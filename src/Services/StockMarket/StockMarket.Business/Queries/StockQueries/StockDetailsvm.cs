using StockMarket.Model.StockModel;
using System.Collections.Generic;

namespace StockMarket.Business.Queries.StockQueries
{
    public class StockDetailsvm
    {
        public IList<Stock> StockList { get; set; }
        public double MinStockPrice { get; set; }
        public double MaxStockPrice { get; set; }
        public double AvgStockPrice { get; set; }
    }
}
