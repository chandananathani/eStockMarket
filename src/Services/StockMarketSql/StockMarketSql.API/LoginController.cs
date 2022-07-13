using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ICommonDetailsBusiness _repository;
        private string generatedToken = null;

        public LoginController(IConfiguration config, ITokenService tokenService, ICommonDetailsBusiness repository)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [AllowAnonymous]
        [HttpGet("{Email}")]
        [ProducesResponseType(typeof(UserInfo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserInfo>> GenerateToken(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("Please provide Email Id");
            }

            IActionResult response = Unauthorized();
            UserInfo validUser = _repository.GetUserDetails(Email);

            if (validUser != null && validUser.Email!=null)
            {
                generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), validUser);

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                    return Ok(generatedToken);
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("User Does not exists");
            }
        }
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