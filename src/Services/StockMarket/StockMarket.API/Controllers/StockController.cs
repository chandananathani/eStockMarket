using StockMarket.API.Entities;
using StockMarket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public StockController(IStockRepository repository, ILogger<StockController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
       
        [HttpPost("{CompanyCode}")]
        [ProducesResponseType(typeof(Stock), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<StockDetails>> add([FromBody] Stock stockDetails, string CompanyCode)
        {
            stockDetails.CompanyCode = CompanyCode;
            await _repository.AddCompanyStockPrice(stockDetails);
            return Ok();

        }
        
        [HttpGet("{CompanyCode}/{StartDate}/{EndDate}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Stock), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stock>> get(string CompanyCode, string StartDate, string EndDate)
        {
            var stockDetails = await _repository.GetCompanyStockPrice(CompanyCode, StartDate,EndDate);
            if (stockDetails == null)
            {
                _logger.LogError($"Stock Details with Company Code:{CompanyCode}, not found");
            }
            return Ok(stockDetails);
        }
    }
}
