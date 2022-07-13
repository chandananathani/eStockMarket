using StockMarkerSql.Data;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Business
{
    public class CommonDetailsBusiness: ICommonDetailsBusiness
    {
       private readonly ICommonDetailsData _context;
        public CommonDetailsBusiness(ICommonDetailsData context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public UserInfo GetUserDetails(string Email)
        {
            return _context.GetUserDetails(Email);
        }
    }
}
