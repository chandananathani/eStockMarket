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
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ICommonRepository _repository;
        private string generatedToken = null;
        private readonly ILogger<TokenController> _logger;

        public TokenController(IConfiguration config, ITokenService tokenService, ICommonRepository repository, ILogger<TokenController> logger)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _repository= repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        
        [AllowAnonymous]
        [HttpGet("{Email}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GenerateToken(string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return BadRequest("Please provide credentials");
                }

                IActionResult response = Unauthorized();
                User validUser = await _repository.GetUserDetails(Email);

                if (validUser != null)
                {
                    generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), validUser);

                    if (generatedToken != null)
                    {
                        HttpContext.Session.SetString("Token", generatedToken);
                        _logger.LogInformation("Token Created Sucessfully");
                        return Ok(generatedToken);
                    }
                    else
                    {
                        _logger.LogWarning("Invalid JWT token");
                        return BadRequest("Invalid JWT Token");
                    }
                }
                else
                {
                    _logger.LogWarning("User does not exists");
                    return BadRequest("User does not exists");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

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