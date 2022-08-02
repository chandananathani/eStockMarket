using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarkerSql.Data
{
    /// <summary>
    /// interface calss for Company details data layer
    /// </summary>
    public interface ICompanyDetailsData
    {
        /// <summary>
        /// method for creating company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>N/A</returns>
        Task RegisterCompany(CompanyDetails company);
    }
}
