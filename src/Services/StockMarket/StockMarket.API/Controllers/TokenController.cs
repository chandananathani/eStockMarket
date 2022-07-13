using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ICommonRepository _repository;
        private string generatedToken = null;

        public TokenController(IConfiguration config, ITokenService tokenService, ICommonRepository repository)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _repository= repository ?? throw new ArgumentNullException(nameof(repository));
        }
        
        
        [AllowAnonymous]
        [HttpGet("{Email}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GenerateToken(string Email)
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
                    return Ok(generatedToken);
                }
                else
                {
                    return BadRequest("Invalid Token");
                }
            }
            else
            {
                return BadRequest("User does not exists");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> Createuser([FromBody] User user)
        {
            await _repository.CreateUser(user);
            return Ok();

        }
    }
}