using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarkerSql.Data
{
    public interface ICommonDetailsData
    {
        UserInfo GetUserDetails(string Email);
    }
}
