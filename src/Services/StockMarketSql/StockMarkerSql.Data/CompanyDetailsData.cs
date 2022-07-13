using Microsoft.Extensions.Configuration;
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
        public CompanyDetailsData(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void RegisterCompany(CompanyDetails companyInfo)
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
    }
}
