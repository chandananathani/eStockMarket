using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Business
{
    /// <summary>
    /// interface for common details business layer
    /// </summary>
    public interface ICommonDetailsBusiness
    {
        /// <summary>
        /// method for get user details
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>UserInfo</returns>
        Task<UserInfo> GetUserDetails(string Email);
    }
}
