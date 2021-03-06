using StockMarket.API.Data;
using StockMarket.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IStockDataContext _context;
        public StockRepository(IStockDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddCompanyStockPrice(Stock stockDetails)
        {
            await _context.StockDetails.InsertOneAsync(stockDetails);
        }

        public async Task<StockDetails> GetCompanyStockPrice(string CompanyCode, string StartDate, string EndDate)
        {
            StockDetails stockDetails = new StockDetails();
            var list= await _context
                         .StockDetails
                         .Find(c => c.CompanyCode == CompanyCode && c.CreatedDate >= Convert.ToDateTime(StartDate) && c.CreatedDate <= Convert.ToDateTime(EndDate))
                         .ToListAsync();           
            double maxStockProce = list.Max(s => s.StockPrice);
            double minStockProce = list.Min(s => s.StockPrice);
            double avgStockPrice = list.Average(s => s.StockPrice);
            stockDetails.StockList = list;
            stockDetails.MaxStockPrice = maxStockProce;
            stockDetails.MinStockPrice = minStockProce;
            stockDetails.AvgStockPrice = avgStockPrice;

            return stockDetails;
                        
        }
    }
}
