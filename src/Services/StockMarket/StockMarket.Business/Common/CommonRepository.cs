using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StockMarket.Data.Common;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMarket.Business.Common
{
    /// <summary>
    /// service class is for <see cref="ICommonRepository"/>
    /// </summary>
    public class CommonRepository : ICommonRepository
    {
        private readonly ICommonDataContext _context;
        private readonly ILogger<CommonRepository> _logger;

        /// <summary>
        /// constructor for CommonRepository
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public CommonRepository(ICommonDataContext context, ILogger<CommonRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// method is for creating user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       public async Task CreateUser(User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                await _context.UserDetails.InsertOneAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <summary>
        /// method is for get user details
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<User> GetUserDetails(string Email)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(c => c.Email, Email);
            try
            {                
                return await _context
                             .UserDetails
                              .Find(filter)
                              .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
