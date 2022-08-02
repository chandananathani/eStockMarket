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
    /// <summary>
    /// Controller class for Company
    /// </summary>
    
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<CompanyController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;


        /// <summary>
        /// Constructor for Company Controller
        /// </summary>
        /// <param name="repository">Specifies to get the object for <see cref="CompanyRepository"/></param>
        /// <param name="logger">The Logger</param>
        /// <param name="config">Specifies to get the object for <see cref="IConfiguration"/></param>
        /// <param name="tokenService">Specifies to get the object for <see cref="TokenService"/></param>
        
        public CompanyController(ICompanyRepository repository, ILogger<CompanyController> logger, IConfiguration config, ITokenService tokenService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));           
        }

        /// <summary>
        /// Method is used for getting all company data
        /// </summary>
        /// <returns>Awaitable task with Company Data</returns>
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Company>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Company>>> getall()
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
                    var companys = await _repository.GetAllCompanys();
                    if (companys.Count()>0)
                    {
                        _logger.LogInformation("getall Api call is succeded");
                        return Ok(companys);
                       
                    }
                    else
                    {
                        _logger.LogError($"Companies are not found");
                        return NotFound(companys);
                    }                   
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

        /// <summary>
        /// Method is used for getting all company data with company Id
        /// </summary>
        /// <param name="CompanyCode">Specifies to get companycode</param>
        /// <returns>Awaitable task with Company Data</returns>

        [HttpGet("{CompanyCode}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Company>> info(string CompanyCode)
        {
            try
            {
                string token = HttpContext.Request.Cookies["Token"];
                Company companys = new Company();
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
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
                        return NotFound(companys);
                    }
                    else
                    {
                        _logger.LogInformation("info Api call is succeded");
                        return Ok(companys);
                    }
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

        /// <summary>
        /// Method is used for Create company
        /// </summary>
        /// <param name="CompanyCode">Specifies to get <see cref="Company"/></param>
        /// <returns>Awaitable task with no Data</returns>

        [HttpPost]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Company>> register([FromBody] Company company)
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

        /// <summary>
        /// Method is used for delete company data with company id
        /// </summary>
        /// <param name="CompanyCode">Specifies to get companycode</param>
        /// <returns>Awaitable task with no return Data</returns>

        [HttpDelete("{CompanyCode}")]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> delete(string CompanyCode)
        {
            try
            {
                string token = HttpContext.Request.Cookies["Token"];
                Company companys = new Company();
                if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                {
                    _logger.LogError("Please Provide JWT token");
                    return BadRequest("Please Provide JWT token");
                }

                if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
                {
                    companys = await _repository.GetCompanybyCode(CompanyCode);
                    if (companys != null)
                    {
                        await _repository.DeleteCompany(CompanyCode);
                        _logger.LogInformation("delete api call is succeded");
                        return Ok("Company Details deleted sucessfully");
                    }
                    else
                    {
                        _logger.LogInformation("Company Id: {CompayCode} not found", CompanyCode);
                        return NotFound("Company Details deleted sucessfully");
                    }
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
