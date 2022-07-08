using AutoMapper;
using StockMarket.Business.Queries.CompanyQueries;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Business.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StockDetails, StockDetailsvm>().ReverseMap();
            CreateMap<Stock, CreateStockCommand>().ReverseMap();           
        }
    }
}
