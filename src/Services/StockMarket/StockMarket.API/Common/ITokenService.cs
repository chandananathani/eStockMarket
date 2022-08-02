using StockMarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Common
{
    /// <summary>
    /// interface calss for Token
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Method used for generating Token
        /// </summary>
        /// <param name="key">Specifies to get key value</param>
        /// <param name="issuer">Specifies to get issuer value</param>
        /// <param name="user">Specifies to get user details</param>
        /// <returns></returns>
        string BuildToken(string key, string issuer, User user);

        /// <summary>
        /// Method used for Validating Token
        /// </summary>
        /// <param name="key">Specifies to get key value</param>
        /// <param name="issuer">Specifies to get issuer value</param>
        /// <param name="audience">Specifies to get audience value</param>
        /// <param name="token">Specifies to get token value</param>
        /// <returns></returns>
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
