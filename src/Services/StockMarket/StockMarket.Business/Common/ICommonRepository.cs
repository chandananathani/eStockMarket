using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockMarket.Business.Common
{
    public interface ICommonRepository
    {
        Task<User> GetUserDetails(string Email);
        Task CreateUser(User user);
    }
}
