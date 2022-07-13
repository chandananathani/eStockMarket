using AutoMapper;
using MediatR;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Business.StockBusiness;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class GetStockListHandler : IRequestHandler<GetStockListQuery, StockDetailsvm>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public GetStockListHandler(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<StockDetailsvm> Handle(GetStockListQuery request, CancellationToken cancellationToken)
        {
            var stockList = await _stockRepository.GetCompanyStockPrice(request.CompanyCode, request.StartDate, request.EndDate);
            return _mapper.Map<StockDetailsvm>(stockList);

        }

    }
}
