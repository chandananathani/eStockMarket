using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.API.Common
{
    /// <summary>
    /// interface class for token
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// method for generating token
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="user"></param>
        /// <returns>Token</returns>
        string BuildToken(string key, string issuer, UserInfo user);

        /// <summary>
        /// method for validating the token
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="token"></param>
        /// <returns>true or false</returns>
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
