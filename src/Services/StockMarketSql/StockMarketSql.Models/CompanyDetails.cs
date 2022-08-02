using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Models
{
    /// <summary>
    /// class for declaring company details
    /// </summary>
    public class CompanyDetails
    {
        /// <summary>
        /// specifies company code
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// specifies company name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// specifies company ceo
        /// </summary>
        public string CompanyCEO { get; set; }

        /// <summary>
        /// specifies company website
        /// </summary>
        public string CompanyWebsite { get; set; }

        /// <summary>
        /// specifies company turnover
        /// </summary>
        public int CompanyTurnover { get; set; }

        /// <summary>
        /// specifies stock exchange
        /// </summary>
        public string StockExchange { get; set; }

        /// <summary>
        /// specifies created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// specifies created by
        /// </summary>
        public string CreatedBy { get; set; }
    }
}
