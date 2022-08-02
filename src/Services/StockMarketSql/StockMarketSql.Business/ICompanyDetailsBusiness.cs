using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Business
{
    /// <summary>
    /// interface for Company details business layer
    /// </summary>
    public interface ICompanyDetailsBusiness
    {
        /// <summary>
        /// method for creating company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>N/A</returns>
        Task RegisterCompany(CompanyDetails company);
    }
}
