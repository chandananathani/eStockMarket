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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ICompanyDataContext _context;
        private readonly IStockDataContext _stockcontext;
        public CompanyRepository(ICompanyDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task RegisterCompany(Company company)
        {
            company.CreatedDate = DateTime.Now;           
            await _context.CompanyDetails.InsertOneAsync(company);
        }
        public async Task<IEnumerable<Company>> GetAllCompanys()
        {
            return await _context
                         .CompanyDetails
                         .Find(c => true)
                         .ToListAsync();
        }
        public async Task<Company> GetCompanybyCode(string CompanyCode)
        {
            return await _context
                         .CompanyDetails
                         .Find(c => c.CompanyCode == CompanyCode)
                         .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCompany(string CompanyCode)
        {
            FilterDefinition<Company> companysfilter = Builders<Company>.Filter.Eq(c => c.CompanyCode, CompanyCode);
            FilterDefinition<Stock> stockfilter = Builders<Stock>.Filter.Eq(c => c.CompanyCode, CompanyCode);
            DeleteResult deleteCompanyResult = await _context.CompanyDetails.DeleteOneAsync(companysfilter);
            DeleteResult deleteStockResult = await _stockcontext.StockDetails.DeleteOneAsync(stockfilter);
            return deleteCompanyResult.IsAcknowledged && deleteCompanyResult.DeletedCount > 0 
                && deleteStockResult.IsAcknowledged&& deleteStockResult.DeletedCount>0;


        }
    }
}
