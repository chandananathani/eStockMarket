using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Business.CompanyBusiness;
using StockMarket.Model.CompanyModel;
using StockMarket.Data.CompanyData;

namespace StockMarket.Business.CompanyBusiness
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ICompanyDataContext _context;
        public CompanyRepository(ICompanyDataContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
        }

        public async Task RegisterCompany(Company company)
        {
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
                         .Find(c=>c.CompanyCode==CompanyCode)
                         .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCompany(string CompanyCode)
        {
            FilterDefinition<Company> filter = Builders<Company>.Filter.Eq(c => c.CompanyCode, CompanyCode);
            DeleteResult deleteResult = await _context.CompanyDetails.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }   
    }
}
