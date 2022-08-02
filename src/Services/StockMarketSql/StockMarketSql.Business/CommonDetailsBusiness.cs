using Microsoft.Extensions.Logging;
using StockMarkerSql.Data;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Business
{
    /// <summary>
    /// service class for <see cref="ICommonDetailsBusiness"/>
    /// </summary>
    public class CommonDetailsBusiness: ICommonDetailsBusiness
    {
       private readonly ICommonDetailsData _context;
        private readonly ILogger<CommonDetailsBusiness> _logger;

        /// <summary>
        /// constructor for CommonDetailsBusiness
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public CommonDetailsBusiness(ICommonDetailsData context, ILogger<CommonDetailsBusiness> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        ///<inheritdoc/>
        public async Task<UserInfo> GetUserDetails(string Email)
        {
            try
            {
                return await _context.GetUserDetails(Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
