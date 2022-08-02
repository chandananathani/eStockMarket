using AutoMapper;
using StockMarket.Business.Queries.CompanyQueries;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Model.StockModel;

namespace StockMarket.Business.Mappings
{
    /// <summary>
    /// class for mapping profile
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// constructor for mapping profile
        /// </summary>
        public MappingProfile()
        {
            CreateMap<StockDetails, StockDetailsvm>().ReverseMap();
            CreateMap<Stock, CreateStockCommand>().ReverseMap();
        }
    }
}
