using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetStockListHandler> _logger;
        public GetStockListHandler(IStockRepository stockRepository, IMapper mapper,ILogger<GetStockListHandler> logger)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<StockDetailsvm> Handle(GetStockListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stockList = await _stockRepository.GetCompanyStockPrice(request.CompanyCode, request.StartDate, request.EndDate);
                return _mapper.Map<StockDetailsvm>(stockList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

    }
}
