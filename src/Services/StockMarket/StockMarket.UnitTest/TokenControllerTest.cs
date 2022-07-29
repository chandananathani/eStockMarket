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
            var controller = new TokenController(_configuration.Object,_tokenService.Object,_commonrepository.Object,_tokenlogger.Object);
            var postId = "chandana.natani@cognizant.com";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GenerateToken_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new TokenController(_configuration.Object, _tokenService.Object, _commonrepository.Object, _tokenlogger.Object);
            var postId = "chandana.nathani@cognizant.com";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GenerateToken_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new TokenController(_configuration.Object, _tokenService.Object, _commonrepository.Object, _tokenlogger.Object);
            var postId = "";

            //Act  
            var data = await controller.GenerateToken(postId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
       
    }
       
}
