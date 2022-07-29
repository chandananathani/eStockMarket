using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using StockMarket.API.Common;
using StockMarket.API.Controllers;
using StockMarket.Business.Common;
using StockMarket.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockMarket.UnitTest
{
    public class TokenControllerTest
    {
        readonly Mock<ICommonRepository> _commonrepository;
        readonly Mock<ILogger<TokenController>> _tokenlogger;
        readonly Mock<IConfiguration> _configuration;
        readonly Mock<ITokenService> _tokenService;

        public TokenControllerTest()
        {
            _commonrepository = new Mock<ICommonRepository>();
            _tokenlogger = new Mock<ILogger<TokenController>>();
            _configuration = new Mock<IConfiguration>();
            _tokenService = new Mock<ITokenService>();
        }

        [Fact]
        public async void Task_GenerateToken_Return_OkResult()
        {
            //Arrange  
            var user = new User() {              
                Email="chandana.natani@cognizant.com"              
            };
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            _commonrepository.Setup<Task<User>>(mock => mock.GetUserDetails(It.IsAny<string>())).Returns(Task.FromResult<User>(user));
            _tokenService.Setup(mock => mock.BuildToken(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<User>())).Returns(token);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            var controller = new TokenController(_configuration.Object, _tokenService.Object, _commonrepository.Object, _tokenlogger.Object);
            var postId = "chandana.natani@cognizant.com";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Fact]
        public async void Task_GenerateToken_Return_NotFoundResult()
        {
            //Arrange  
            var user = new User();
            string token = "";
            _commonrepository.Setup<Task<User>>(mock => mock.GetUserDetails(It.IsAny<string>())).Returns(Task.FromResult<User>(user));
            _tokenService.Setup(mock => mock.BuildToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>())).Returns(token);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            var controller = new TokenController(_configuration.Object, _tokenService.Object, _commonrepository.Object, _tokenlogger.Object);
            var postId = "chandana.nathani@cognizant.com";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }

        [Fact]
        public async void Task_GenerateToken_Return_BadRequestResult()
        {
            //Arrange  
            var user = new User()
            {
                Email = "chandana.nathani@cognizant.com"
            };
            string token = "";
            _commonrepository.Setup<Task<User>>(mock => mock.GetUserDetails(It.IsAny<string>())).Returns(Task.FromResult<User>(user));
            _tokenService.Setup(mock => mock.BuildToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>())).Returns(token);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            var controller = new TokenController(_configuration.Object, _tokenService.Object, _commonrepository.Object, _tokenlogger.Object);
            var postId = "chandana.natani@cognizant.com";
            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }
       
    }
       
}
