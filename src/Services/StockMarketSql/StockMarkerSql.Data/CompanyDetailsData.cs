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
    /// service class for <see cref="ICompanyDetailsData"/>
    /// </summary>
    public class CompanyDetailsData : ICompanyDetailsData
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CompanyDetailsData> _logger;

        /// <summary>
        /// constructor for CompanyDetailsData
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public CompanyDetailsData(IConfiguration configuration, ILogger<CompanyDetailsData> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task RegisterCompany(CompanyDetails companyInfo)
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:StockmarketDatabase"];
                
                int i = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("CreateCompany", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyCode", companyInfo.CompanyCode);
                    cmd.Parameters.AddWithValue("@CompanyName", companyInfo.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyCEO", companyInfo.CompanyCEO);
                    cmd.Parameters.AddWithValue("@CompanyTurnover", companyInfo.CompanyTurnover);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", companyInfo.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@StockExchange", companyInfo.StockExchange);
                    cmd.Parameters.AddWithValue("@CreatedBy", companyInfo.CreatedBy);

                    connection.Open();
                    i = cmd.ExecuteNonQuery();
                    connection.Close();
                    _logger.LogInformation("Company details created sucessfully for {CompanyCode}",companyInfo.CompanyCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }
        }
    }
}
