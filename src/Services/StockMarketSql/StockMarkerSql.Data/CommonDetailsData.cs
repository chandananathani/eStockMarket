using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarkerSql.Data
{
    /// <summary>
    /// service class for <see cref="ICommonDetailsData"/>
    /// </summary>
    public class CommonDetailsData : ICommonDetailsData
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommonDetailsData> _logger;

        /// <summary>
        /// constructor for CommonDetailsData
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public CommonDetailsData(IConfiguration configuration, ILogger<CommonDetailsData> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<UserInfo> GetUserDetails(string email)
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:StockmarketDatabase"];
                UserInfo userInfo = new UserInfo();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //SqlDataReader
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetUserDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        userInfo.UserId = (string)(ds.Tables[0].Rows[0]["UserId"]);
                        userInfo.FirstName = (string)(ds.Tables[0].Rows[0]["FirstName"]);
                        userInfo.LastName = (string)(ds.Tables[0].Rows[0]["LastName"]);
                        userInfo.UserName = (string)(ds.Tables[0].Rows[0]["UserName"]);
                        userInfo.Email = (string)(ds.Tables[0].Rows[0]["Email"]);
                        userInfo.Password = (string)(ds.Tables[0].Rows[0]["Password"]);
                        userInfo.IsActive = (Int32)(ds.Tables[0].Rows[0]["IsActive"]);
                        userInfo.CreatedDate = (DateTime)(ds.Tables[0].Rows[0]["CreatedDate"]);
                    }
                    connection.Close();
                }
                return userInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
