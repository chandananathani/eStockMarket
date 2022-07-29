using Microsoft.Extensions.Logging;
using StockMarkerSql.Data;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarketSql.Business
{
    public class CompanyDetailsBusiness: ICompanyDetailsBusiness
    {
       private readonly ICompanyDetailsData _context;
        private readonly ILogger<CompanyDetailsBusiness> _logger;
        public CompanyDetailsBusiness(ICompanyDetailsData context, ILogger<CompanyDetailsBusiness> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void RegisterCompany(CompanyDetails company)
        {
            try
            {
                company.CreatedDate = DateTime.Now;
                _context.RegisterCompany(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);                
            }
        }
    }
}
