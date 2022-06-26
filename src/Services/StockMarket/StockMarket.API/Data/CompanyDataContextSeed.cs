using StockMarket.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Data
{
    public class CompanyDataContextSeed
    {
        public static void CompanysSeedData(IMongoCollection<Company> companyCollection)
        {
            companyCollection.Find(c => true).Any();
            //bool existCompany = companyCollection.Find(c => true).Any();
            //if (!existCompany)
            //{
            //    companyCollection.InsertManyAsync(GetPreconfiguredCompanys());
            //}
        }
       private static IEnumerable<Company> GetPreconfiguredCompanys()
        {
            return new List<Company>()
             {
                new Company()
                {
                CompanyCode="C001",
                CompanyName="Company1",
                CompanyCEO="CEO1",
                CompanyWebsite="www.company1.com",
                CompanyTurnover="20M",
                StockExchange="NSE",
                CreatedDate=DateTime.Now
                },
                new Company()
                {
                CompanyCode="C002",
                CompanyName="Company2",
                CompanyCEO="CEO2",
                CompanyWebsite="www.company2.com",
                CompanyTurnover="30M",
                StockExchange="BSE",
                CreatedDate=DateTime.Now
                },
                new Company()
                {
                CompanyCode="C003",
                CompanyName="Company3",
                CompanyCEO="CEO3",
                CompanyWebsite="www.company3.com",
                CompanyTurnover="40M",
                StockExchange="HSE",
                CreatedDate=DateTime.Now
                },
                new Company()
                {
                CompanyCode="C004",
                CompanyName="Company4",
                CompanyCEO="CEO4",
                CompanyWebsite="www.company4.com",
                CompanyTurnover="50M",
                StockExchange="DSE",
                 CreatedDate=DateTime.Now
                },
                new Company()
                {
                CompanyCode="C005",
                CompanyName="Company5",
                CompanyCEO="CEO5",
                CompanyWebsite="www.company5.com",
                CompanyTurnover="10M",
                StockExchange="ZSE",
                 CreatedDate=DateTime.Now
                }
            };
        }

    }
}