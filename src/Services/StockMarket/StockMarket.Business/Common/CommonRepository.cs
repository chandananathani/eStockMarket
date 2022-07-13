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
       public CommonRepository(ICommonDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateUser(User user)
        {
            user.CreatedDate = DateTime.Now;
            await _context.UserDetails.InsertOneAsync(user);
        }

        public async Task<User> GetUserDetails(string Email)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(c=>c.Email,Email);
            var test= _context
                         .UserDetails.Find(c => true)
                         .ToListAsync();
            return  await _context
                         .UserDetails
                          .Find(filter)
                          .FirstOrDefaultAsync();
        }
    }
}
