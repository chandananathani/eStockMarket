using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StockMarket.API.Common;
using StockMarket.Business.Common;
using StockMarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.API.Controllers
{
    /// <summary>
    /// Controller class for Token
    /// </summary>

    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ICommonRepository _repository;
        private string generatedToken = null;
        private readonly ILogger<TokenController> _logger;

        /// <summary>
        /// Constructor for Token Controller
        /// </summary>
        /// <param name="config">Specifies to get the object for <see cref="IConfiguration</param>
        /// <param name="tokenService">Specifies to get the object for <see cref="TokenService"/></param>
        /// <param name="repository">Specifies to get the object for <see cref="ICommonRepository"/></param>
        /// <param name="logger">The logger</param>
        public TokenController(IConfiguration config, ITokenService tokenService, ICommonRepository repository, ILogger<TokenController> logger)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _repository= repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Method to generate token
        /// </summary>
        /// <param name="Email">Specifies to get email</param>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpGet("{Email}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GenerateToken(string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    _logger.LogError("Email empty");
                    return BadRequest("Please provide credentials");
                }

                User validUser = await _repository.GetUserDetails(Email);

                if (validUser != null && !string.IsNullOrEmpty(validUser.Email) && !string.IsNullOrWhiteSpace(validUser.Email))
                {
                    generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), validUser);

                    if (!string.IsNullOrEmpty(generatedToken) && !string.IsNullOrWhiteSpace(generatedToken))
                    {
                        //HttpContext.Session.SetString("Token", generatedToken);
                        _logger.LogInformation("Token Created Sucessfully");
                        return Ok(generatedToken);
                    }
                    else
                    {
                        _logger.LogError("Invalid JWT token");
                        return BadRequest("Invalid JWT Token");
                    }
                }
                else
                {
                    _logger.LogWarning("User does not exists");
                    return NotFound("User does not exists");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to create user
        /// </summary>
        /// <param name="User">Specifies to get<see cref="User"/></param>
        /// <returns></returns>

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> Createuser([FromBody] User user)
        {
            try
            {
                await _repository.CreateUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}