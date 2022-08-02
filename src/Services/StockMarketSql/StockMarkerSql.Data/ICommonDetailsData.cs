using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarkerSql.Data
{
    /// <summary>
    /// interface calss for Common details data layer
    /// </summary>
    public interface ICommonDetailsData
    {
        /// <summary>
        /// methid is for fetching user details
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>UserInfo</returns>
        Task<UserInfo> GetUserDetails(string Email);
    }
}
