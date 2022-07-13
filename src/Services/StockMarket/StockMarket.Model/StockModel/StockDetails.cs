using System.Collections.Generic;

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
