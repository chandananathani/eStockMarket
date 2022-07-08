using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Business.StockBusiness
{
    public interface IStockRepository
    {
        Task AddCompanyStockPrice(Stock company);
        Task<StockDetails> GetCompanyStockPrice(string CompanyCode, string StartDate, string EndDate);
    }
}
