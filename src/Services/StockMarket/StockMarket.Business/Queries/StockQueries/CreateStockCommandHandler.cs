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
    /// <summary>
    /// class is for create stock command handler
    /// </summary>
    public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, string>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateStockCommandHandler> _logger;

        /// <summary>
        /// constructor for CreateStockCommandHandler
        /// </summary>
        /// <param name="stockRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public CreateStockCommandHandler(IStockRepository stockRepository, IMapper mapper, ILogger<CreateStockCommandHandler> logger)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// method for to handle the create stock command
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Company Code</returns>
        public async Task<string> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var stockEntity = _mapper.Map<Stock>(request);
                stockEntity.CreatedDate = DateTime.Now;                
                await _stockRepository.AddCompanyStockPrice(stockEntity);

                _logger.LogInformation($"Stock details for {stockEntity.CompanyCode} is successfully created.");

                return stockEntity.CompanyCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
