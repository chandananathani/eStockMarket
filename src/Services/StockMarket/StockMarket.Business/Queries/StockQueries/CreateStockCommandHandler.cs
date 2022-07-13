using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StockMarket.Business.StockBusiness;
using StockMarket.Model.StockModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, string>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStockCommandHandler> _logger;
        public CreateStockCommandHandler(IStockRepository stockRepository, IMapper mapper, ILogger<CreateStockCommandHandler> logger)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<string> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var stockEntity = _mapper.Map<Stock>(request);
            stockEntity.CreatedDate = DateTime.Now;
            stockEntity.CreatedBy = "Chandana";
            await _stockRepository.AddCompanyStockPrice(stockEntity);

            _logger.LogInformation($"Stock details for {stockEntity.CompanyCode} is successfully created.");

            return stockEntity.CompanyCode;
        }
    }
}
