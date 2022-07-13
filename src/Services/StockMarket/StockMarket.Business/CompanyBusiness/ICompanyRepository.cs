using StockMarket.Model.CompanyModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMarket.Business.CompanyBusiness
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanys();
        Task<Company> GetCompanybyCode(string CompanyCode);
        Task RegisterCompany(Company company);
        Task<bool> DeleteCompany(string CompanyCode);

    }
}
