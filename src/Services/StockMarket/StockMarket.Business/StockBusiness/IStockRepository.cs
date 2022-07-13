using StockMarket.Model.StockModel;
using System.Threading.Tasks;

namespace StockMarket.Business.StockBusiness
{
    public interface IStockRepository
    {
        Task AddCompanyStockPrice(Stock company);
        Task<StockDetails> GetCompanyStockPrice(string CompanyCode, string StartDate, string EndDate);
    }
}
