using StockMarket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Repositories
{
    public interface IStockRepository
    {
        Task AddCompanyStockPrice(Stock company);
        Task<StockDetails> GetCompanyStockPrice(string CompanyCode, string StartDate, string EndDate);
    }
}
