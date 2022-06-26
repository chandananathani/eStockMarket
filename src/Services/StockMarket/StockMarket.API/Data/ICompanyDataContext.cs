using StockMarket.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.API.Data
{
    public interface ICompanyDataContext
    {
        IMongoCollection<Company> CompanyDetails { get; }       
    }
}
