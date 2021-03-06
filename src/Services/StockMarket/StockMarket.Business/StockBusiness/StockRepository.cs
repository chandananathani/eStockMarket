using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StockMarket.Data.StockData;
using StockMarket.Model.StockModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Business.StockBusiness
{
    /// <summary>
    /// service class for <see cref="IStockRepository"/>
    /// </summary>
    public class StockRepository : IStockRepository
    {
        private readonly IStockDataContext _context;
        private readonly ILogger<StockRepository> _logger;

        /// <summary>
        /// constructor for StockRepository
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public StockRepository(IStockDataContext context, ILogger<StockRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task AddCompanyStockPrice(Stock stockDetails)
        {
            try
            {
                await _context.StockDetails.InsertOneAsync(stockDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }
        }

        /// <inheritdoc/>
        public async Task<StockDetails> GetCompanyStockPrice(string CompanyCode, string StartDate, string EndDate)
        {
            try
            {
                StockDetails stockDetails = new StockDetails();
                var list = await _context
                             .StockDetails
                             .Find(c => c.CompanyCode == CompanyCode && c.CreatedDate >= Convert.ToDateTime(StartDate) && c.CreatedDate <= Convert.ToDateTime(EndDate))
                             .ToListAsync();
                if (list != null && list.Count > 0)
                {
                    double maxStockProce = list.Max(s => s.StockPrice);
                    double minStockProce = list.Min(s => s.StockPrice);
                    double avgStockPrice = list.Average(s => s.StockPrice);
                    stockDetails.StockList = list;
                    stockDetails.MaxStockPrice = maxStockProce;
                    stockDetails.MinStockPrice = minStockProce;
                    stockDetails.AvgStockPrice = avgStockPrice;
                }

                return stockDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
