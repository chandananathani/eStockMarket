using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockMarket.API.Common;
using StockMarket.Business.CompanyBusiness;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<CompanyController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
       
        public CompanyController(ICompanyRepository repository, ILogger<CompanyController> logger, IConfiguration config, ITokenService tokenService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Company>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Company>>> getall()
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (token == null)
                {
                    _logger.LogError("Please Provide JWT token");
                    return BadRequest("Please Provide JWT token");
                }

                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    var companys = await _repository.GetAllCompanys();
                    _logger.LogInformation("getall Api call is succeded");
                    return Ok(companys);
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
        [HttpGet("{CompanyCode}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Company>> info(string CompanyCode)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                Company companys = new Company();
                if (token == null)
                {
                    _logger.LogError("Please Provide JWT token");
                    return BadRequest("Please Provide JWT token");
                }

                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    companys = await _repository.GetCompanybyCode(CompanyCode);
                    if (companys == null)
                    {
                        _logger.LogError($"Company with Company Code:{CompanyCode}, not found");
                    }
                    _logger.LogInformation("info Api call is succeded");
                    return Ok(companys);
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
        [HttpPost]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Company>> register([FromBody] Company company)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (token == null)
                {
                    _logger.LogError("Please Provide JWT token");
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
        [HttpDelete("{CompanyCode}")]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> delete(string CompanyCode)
        {
            try
            {
                string token = HttpContext.Session.GetString("Token");
                if (token == null)
                {
                    _logger.LogError("Please Provide JWT token");
                    return BadRequest("Please Provide JWT token");
                }

                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    await _repository.DeleteCompany(CompanyCode);
                    _logger.LogInformation("delete api call is succeded");
                    return Ok("Company Details deleted sucessfully");
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
