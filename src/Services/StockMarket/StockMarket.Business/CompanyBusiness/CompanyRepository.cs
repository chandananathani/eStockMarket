using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StockMarket.Data.CompanyData;
using StockMarket.Data.StockData;
using StockMarket.Model.CompanyModel;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMarket.Business.CompanyBusiness
{
    /// <summary>
    /// service class for <see cref="ICompanyRepository"/>
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ICompanyDataContext _context;
        private readonly IStockDataContext _stockcontext;
        private readonly ILogger<CompanyRepository> _logger;

        /// <summary>
        /// constructor for CompanyRepositiory
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public CompanyRepository(ICompanyDataContext context, ILogger<CompanyRepository> logger, IStockDataContext stockDataContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stockcontext= stockDataContext ?? throw new ArgumentNullException(nameof(stockDataContext));
        }

        /// <inheritdoc/>
        public async Task RegisterCompany(Company company)
        {
            try
            {
                company.CreatedDate = DateTime.Now;
                await _context.CompanyDetails.InsertOneAsync(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Company>> GetAllCompanys()
        {   
            try
            {
                return await _context
                             .CompanyDetails
                             .Find(c => true)
                             .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<Company> GetCompanybyCode(string CompanyCode)
        {
            try
            {
                return await _context
                             .CompanyDetails
                             .Find(c => c.CompanyCode == CompanyCode)
                             .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteCompany(string CompanyCode)
        {
            try
            {
                FilterDefinition<Company> companysfilter = Builders<Company>.Filter.Eq(c => c.CompanyCode, CompanyCode);
                FilterDefinition<Stock> stockfilter = Builders<Stock>.Filter.Eq(c => c.CompanyCode, CompanyCode);
                DeleteResult deleteCompanyResult = await _context.CompanyDetails.DeleteOneAsync(companysfilter);
                DeleteResult deleteStockResult = await _stockcontext.StockDetails.DeleteOneAsync(stockfilter);
                return deleteCompanyResult.IsAcknowledged && deleteCompanyResult.DeletedCount > 0
                    && deleteStockResult.IsAcknowledged && deleteStockResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
