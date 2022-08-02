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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockMarketSql.API
{
    /// <summary>
    /// controller class for login
    /// </summary>
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ICommonDetailsBusiness _repository;
        private string generatedToken = null;
        private readonly ILogger<LoginController> _logger;

        /// <summary>
        /// constructor for LoginController
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tokenService"></param>
        /// <param name="repository"></param>
        public LoginController(IConfiguration config, ITokenService tokenService, ICommonDetailsBusiness repository, ILogger<LoginController> logger)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// method for generating token
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>Token</returns>
        [AllowAnonymous]
        [HttpGet("{Email}")]
        [ProducesResponseType(typeof(UserInfo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserInfo>> GenerateToken(string Email)
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    _logger.LogError("Email empty");
                    return BadRequest("Please provide credentials");
                }

                UserInfo validUser = await _repository.GetUserDetails(Email);

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
        /// method for fetching user details from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private UserInfo GetUserDetails(string email)
        {
            string connectionString = _configuration["ConnectionStrings:StockmarketDatabase"];
            UserInfo userInfo = new UserInfo();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();
                SqlCommand command = new SqlCommand("GetUserDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    userInfo.UserId = (string)(ds.Tables[0].Rows[0]["UserId"]);
                    userInfo.FirstName = (string)(ds.Tables[0].Rows[0]["FirstName"]);
                    userInfo.LastName = (string)(ds.Tables[0].Rows[0]["LastName"]);
                    userInfo.UserName = (string)(ds.Tables[0].Rows[0]["UserName"]);
                    userInfo.Email = (string)(ds.Tables[0].Rows[0]["Email"]);
                    userInfo.Password = (string)(ds.Tables[0].Rows[0]["Password"]);
                    userInfo.IsActive = (Int32)(ds.Tables[0].Rows[0]["IsActive"]);
                    userInfo.CreatedDate = (DateTime)(ds.Tables[0].Rows[0]["CreatedDate"]);
                }
                connection.Close();
            }
            return userInfo;
        }
    }
}