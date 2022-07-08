using MongoDB.Driver;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Data.CompanyData
{
    public interface ICompanyDataContext
    {
        IMongoCollection<Company> CompanyDetails { get; }       
    }
}
