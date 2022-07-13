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
        public CompanyDetailsBusiness(ICompanyDetailsData context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void RegisterCompany(CompanyDetails company)
        {
            company.CreatedDate = DateTime.Now;
            _context.RegisterCompany(company);
        }
    }
}
