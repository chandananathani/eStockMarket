using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using StockMarketSql.API;
using StockMarketSql.API.Common;
using StockMarketSql.Business;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockMarketSql.UnitTest
{
    /// <summary>
    /// Test class for <see cref="LoginController"/>
    /// </summary>
    public class LoginControllerTest
    {
        readonly Mock<ICommonDetailsBusiness> _commonbusiness;
        readonly Mock<ILogger<LoginController>> _loginlogger;
        readonly Mock<IConfiguration> _configuration;
        readonly Mock<ITokenService> _tokenService;

        /// <summary>
        /// constructor for LoginControllerTest
        /// </summary>
        public LoginControllerTest()
        {
            _commonbusiness = new Mock<ICommonDetailsBusiness>();
            _loginlogger = new Mock<ILogger<LoginController>>();
            _configuration = new Mock<IConfiguration>();
            _tokenService = new Mock<ITokenService>();
        }

        /// <summary>
        /// To verify GenerateToken with valid data and verify to return Ok result
        /// </summary>
        [Fact]
        public async void Task_GenerateToken_Return_OkResult()
        {
            //Arrange  
            var user = new UserInfo()
            {
                Email = "chandana.nathani@cognizant.com"
            };
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            _commonbusiness.Setup<Task<UserInfo>>(mock => mock.GetUserDetails(It.IsAny<string>())).Returns(Task.FromResult<UserInfo>(user));
            _tokenService.Setup(mock => mock.BuildToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserInfo>())).Returns(token);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            var controller = new LoginController(_configuration.Object, _tokenService.Object, _commonbusiness.Object,_loginlogger.Object);
            var postId = "chandana.nathani@cognizant.com";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        /// <summary>
        /// To verify GenerateToken with no data and verify to return NotFound result
        /// </summary>
        [Fact]
        public async void Task_GenerateToken_Return_NotFoundResult()
        {
            //Arrange  
            var user = new UserInfo();
            string token = "";
            _commonbusiness.Setup<Task<UserInfo>>(mock => mock.GetUserDetails(It.IsAny<string>())).Returns(Task.FromResult<UserInfo>(user));
            _tokenService.Setup(mock => mock.BuildToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserInfo>())).Returns(token);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            var controller = new LoginController(_configuration.Object, _tokenService.Object, _commonbusiness.Object, _loginlogger.Object);
            var postId = "chandana.nathani@cognizant.com";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }

        /// <summary>
        /// To verify GenerateToken with in data and verify to return BadRequest result
        /// </summary>
        [Fact]
        public async void Task_GenerateToken_Return_BadRequestResult()
        {
            //Arrange  
            var user = new UserInfo()
            {
                Email = "chandana.nathani@cognizant.com"
            };
            string token = "";
            _commonbusiness.Setup<Task<UserInfo>>(mock => mock.GetUserDetails(It.IsAny<string>())).Returns(Task.FromResult<UserInfo>(user));
            _tokenService.Setup(mock => mock.BuildToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserInfo>())).Returns(token);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            var controller = new LoginController(_configuration.Object, _tokenService.Object, _commonbusiness.Object, _loginlogger.Object);
            var postId = "chandana.nathani@cognizant.com";
            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }
    }
}
