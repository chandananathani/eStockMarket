using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockMarket.Business.Common
{
    /// <summary>
    /// interface class for common details
    /// </summary>
    public interface ICommonRepository
    {
        /// <summary>
        /// method is for ger user details
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        Task<User> GetUserDetails(string Email);

        /// <summary>
        /// method is for creating the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task CreateUser(User user);
    }
}
