using StockMarket.Model.StockModel;
using System.Threading.Tasks;

namespace StockMarket.Business.StockBusiness
{
    /// <summary>
    /// interface class for stock business layer
    /// </summary>
    public interface IStockRepository
    {
        /// <summary>
        /// method is for adding stock price details
        /// </summary>
        /// <param name="company"></param>
        /// <returns>N/A</returns>
        Task AddCompanyStockPrice(Stock company);

        /// <summary>
        /// method for getting company stock price
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>Stock Details</returns>
        Task<StockDetails> GetCompanyStockPrice(string CompanyCode, string StartDate, string EndDate);
    }
}
