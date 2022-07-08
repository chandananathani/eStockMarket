using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Model.StockModel
{
    public class StockDetails
    {
       public IList<Stock> StockList { get; set; }
        public double MinStockPrice { get; set; }
        public double MaxStockPrice { get; set; }
        public double AvgStockPrice { get; set; }
    }
}
