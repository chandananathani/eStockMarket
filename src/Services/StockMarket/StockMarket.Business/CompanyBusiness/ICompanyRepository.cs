using StockMarket.Model.CompanyModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMarket.Business.CompanyBusiness
{
    /// <summary>
    /// interface class for company business layer
    /// </summary>
    public interface ICompanyRepository
    {
        /// <summary>
        /// method is for getting all companys
        /// </summary>
        /// <returns>Company object</returns>
        Task<IEnumerable<Company>> GetAllCompanys();

        /// <summary>
        /// method is for getting company details by companycode
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <returns>Company object</returns>
        Task<Company> GetCompanybyCode(string CompanyCode);

        /// <summary>
        /// method is for creating company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>N/A</returns>
        Task RegisterCompany(Company company);


        /// <summary>
        /// method is for deleting company details by companycode
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <returns>true or flase</returns>
        Task<bool> DeleteCompany(string CompanyCode);

    }
}
