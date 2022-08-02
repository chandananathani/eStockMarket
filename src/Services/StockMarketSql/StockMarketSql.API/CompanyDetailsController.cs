using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockMarketSql.API.Common;
using StockMarketSql.Business;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockMarketSql.API
{
    /// <summary>
    /// controller class for Company details
    /// </summary>
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CompanyDetailsController : ControllerBase
    {
        private readonly ICompanyDetailsBusiness _repository;
        private readonly ILogger<CompanyDetailsController> _logger;
        public  readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// constructor for CompanyDetailsController
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        /// <param name="tokenService"></param>
        public CompanyDetailsController(ICompanyDetailsBusiness repository, ILogger<CompanyDetailsController> logger, IConfiguration config, ITokenService tokenService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

       
        /// <summary>
        /// method for creating company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>N/A</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyDetails>> register([FromBody] CompanyDetails company)
        {
            try
            {
                string token = HttpContext.Request.Cookies["Token"];
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                {
                    return BadRequest("Please Provide JWT token");
                }

                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    await _repository.RegisterCompany(company);
                    _logger.LogInformation("register Api call is succeded");
                    return Ok("Company Details Created Sucessfully");
                }
                else
                {
                    _logger.LogError("Invalid JWT token");
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
