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
    public class CompanyDetailsData : ICompanyDetailsData
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CompanyDetailsData> _logger;
        public CompanyDetailsData(IConfiguration configuration, ILogger<CompanyDetailsData> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async void RegisterCompany(CompanyDetails companyInfo)
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:StockmarketDatabase"];
                CompanyDetails userInfo = new CompanyDetails();
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
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }
        }
    }
}
