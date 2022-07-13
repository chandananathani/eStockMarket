using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Models
{
    public class CompanyDetails
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCEO { get; set; }
        public string CompanyWebsite { get; set; }
        public int CompanyTurnover { get; set; }
        public string StockExchange { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
