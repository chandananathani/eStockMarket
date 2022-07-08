using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
