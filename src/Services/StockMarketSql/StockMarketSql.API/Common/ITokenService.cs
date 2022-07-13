using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.API.Common
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserInfo user);
        bool ValidateToken(string key, string issuer, string audience, string token);
    }
}
