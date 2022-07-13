using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockMarket.API.Common;
using StockMarket.Business.Queries.CompanyQueries;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Business.StockBusiness;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _repository;
        private readonly ILogger<StockController> _logger;
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;
        public readonly IConfiguration _configuration;
        public StockController(IStockRepository repository, ILogger<StockController> logger, IMediator mediator, ITokenService tokenService, IConfiguration config)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stock>> add([FromBody] CreateStockCommand command)
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return BadRequest("Please Provide JWT token");
            }
            if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest("Invalid JWT token");
            }
            

        }

        [Authorize]
        [HttpGet("{CompanyCode}/{StartDate}/{EndDate}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<StockDetailsvm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stock>> get(string CompanyCode, string StartDate, string EndDate)
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return BadRequest("Please Provide JWT token");
            }
            if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
            {
                var stockDetails = new GetStockListQuery(CompanyCode, StartDate, EndDate);
                var _stockDetails = await _mediator.Send(stockDetails);
                if (_stockDetails == null)
                {
                    _logger.LogError($"Stock Details with Company Code:{CompanyCode}, not found");
                }
                return Ok(_stockDetails);
            }
            else
            {
                return BadRequest("Invalid JWT token");
            }
        }
    }
}
