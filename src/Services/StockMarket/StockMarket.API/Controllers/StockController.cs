using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockMarket.Business.Queries.CompanyQueries;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Business.StockBusiness;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class StockController:ControllerBase
    {
        private readonly IStockRepository _repository;
        private readonly ILogger<StockController> _logger;
        private readonly IMediator _mediator;
        public StockController(IStockRepository repository, ILogger<StockController> logger, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
       
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stock>> add([FromBody] CreateStockCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
            //stockDetails.CompanyCode = CompanyCode;
            //await _repository.AddCompanyStockPrice(stockDetails);
            //return Ok();

        }
        
        [HttpGet("{CompanyCode}/{StartDate}/{EndDate}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<StockDetailsvm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stock>> get(string CompanyCode, string StartDate, string EndDate)
        {
            var stockDetails = new GetStockListQuery(CompanyCode, StartDate,EndDate);
            var _stockDetails = await _mediator.Send(stockDetails);
            if (_stockDetails == null)
            {
                _logger.LogError($"Stock Details with Company Code:{CompanyCode}, not found");
            }
            return Ok(_stockDetails);
        }
    }
}
