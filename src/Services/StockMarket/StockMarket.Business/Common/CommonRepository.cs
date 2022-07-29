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
    public class CommonRepository : ICommonRepository
    {
        private readonly ICommonDataContext _context;
        private readonly ILogger<CommonRepository> _logger;
        public CommonRepository(ICommonDataContext context, ILogger<CommonRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        List<User> user = new List<User>();
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
