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
            try
            {
                string token = HttpContext.Request.Cookies["Token"];
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                {
                    _logger.LogError("Please Provide JWT token");
                    return BadRequest("Please Provide JWT token");
                }
                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    var result = await _mediator.Send(command);
                    _logger.LogInformation("add Api call is succeded");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("Invalid JWT token");
                    return BadRequest("Invalid JWT token");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpGet("{CompanyCode}/{StartDate}/{EndDate}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<StockDetailsvm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stock>> get(string CompanyCode, string StartDate, string EndDate)
        {
            try
            {
                string token = HttpContext.Request.Cookies["Token"];
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                {
                    _logger.LogError("Please Provide JWT token");
                    return BadRequest("Please Provide JWT token");
                }
                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    var stockDetails = new GetStockListQuery(CompanyCode, StartDate, EndDate);
                    var _stockDetails = await _mediator.Send(stockDetails);
                    if (_stockDetails == null)
                    {
                        _logger.LogError($"Stock Details with Company Code:{CompanyCode}, not found");
                        return NotFound(_stockDetails);
                    }
                    else
                    {
                        _logger.LogInformation("get Api call is succeded");
                        return Ok(_stockDetails);
                    }
                }
                else
                {
                    _logger.LogWarning("Invalid JWT token");
                    return BadRequest("Invalid JWT token");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
