using StockMarket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanys();
        Task<Company> GetCompanybyCode(string CompanyCode);
        Task RegisterCompany(Company company);
        Task<bool> DeleteCompany(string CompanyCode);     

    }
}
