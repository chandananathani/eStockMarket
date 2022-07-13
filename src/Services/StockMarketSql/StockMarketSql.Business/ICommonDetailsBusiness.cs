using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Business
{
    public interface ICommonDetailsBusiness
    {
        UserInfo GetUserDetails(string Email);
    }
}
