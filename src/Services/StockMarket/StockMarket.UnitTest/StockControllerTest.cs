using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Moq;
using StockMarket.API.Common;
using StockMarket.API.Controllers;
using StockMarket.Business.Queries.CompanyQueries;
using StockMarket.Business.Queries.StockQueries;
using StockMarket.Business.StockBusiness;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StockMarket.UnitTest
{
    /// <summary>
    /// Test class for <see cref="StockController"/>
    /// </summary>
    public class StockControllerTest
    {
        readonly Mock<IStockRepository> _repository;
        readonly Mock<ILogger<StockController>> _logger;
        readonly Mock<IConfiguration> _configuration;
        readonly Mock<ITokenService> _tokenService;
        readonly StockController controller;
        readonly Mock<IMediator> _mediator;

        /// <summary>
        /// constructor for StockControllerTest
        /// </summary>
        public StockControllerTest()
        {
            _repository = new Mock<IStockRepository>();
            _logger = new Mock<ILogger<StockController>>();
            _configuration = new Mock<IConfiguration>();
            _tokenService = new Mock<ITokenService>();
            _mediator = new Mock<IMediator>();
            controller = new StockController(_repository.Object, _logger.Object,_mediator.Object, _tokenService.Object,_configuration.Object);
        }
        #region Get By Company Id, start date, end date 

        /// <summary>
        /// To verify GetStockById with valid data and returns OK result
        /// </summary>
        [Fact]
        public async void Task_GetStockById_Return_OkResult()
        {
            //Arrange  
            StockDetailsvm stockDetails = new StockDetailsvm() { StockList = new List<Stock>() };
            StockDetails stock = new StockDetails();
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock=>mock.GetCompanyStockPrice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<StockDetails>(stock));
            _mediator.Setup(m => m.Send(It.IsAny<GetStockListQuery>(),It.IsAny<CancellationToken>())).Returns(Task.FromResult<StockDetailsvm>(stockDetails));
             
            var companyId = "C001";
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today.AddDays(3);

            //Act  
            var data = await controller.get(companyId,startDate.ToString(),endDate.ToString());

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        /// <summary>
        /// To verify GetStockById with no data and returns NotFound result
        /// </summary>
        [Fact]
        public async void Task_GetStockById_Return_NotFoundResult()
        {
            //Arrange  
            StockDetailsvm stockDetails = null;
            StockDetails stock = new StockDetails();
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.GetCompanyStockPrice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<StockDetails>(stock));
            _mediator.Setup(m => m.Send(It.IsAny<GetStockListQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<StockDetailsvm>(stockDetails));


            var companyId = "C001";
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today.AddDays(3);
           
            //Act  
            var data = await controller.get(companyId, startDate.ToString(), endDate.ToString());

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }

        /// <summary>
        /// To verify GetStockById with in valid data and returns BadResult result
        /// </summary>
        [Fact]
        public async void Task_GetStockById_Return_BadRequestResult()
        {
            //Arrange  
            StockDetailsvm stockDetails = new StockDetailsvm() { StockList = new List<Stock>() };
            StockDetails stock = new StockDetails();
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.GetCompanyStockPrice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<StockDetails>(stock));
            _mediator.Setup(m => m.Send(It.IsAny<GetStockListQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<StockDetailsvm>(stockDetails));

            var companyId = "C001";
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today.AddDays(3);

            //Act  
            var data = await controller.get(companyId,startDate.ToString(),endDate.ToString());

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        #endregion

        #region Create Stocks

        /// <summary>
        /// To verify Add with valid data and returns OK result
        /// </summary>
        [Fact]
        public async void Task_Add_ValidStockData_Return_OkResult()
        {
            //Arrange  
            string stockDetails = "";
            StockDetails stock = new StockDetails();
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.GetCompanyStockPrice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<StockDetails>(stock));
            _mediator.Setup(m => m.Send(It.IsAny<CreateStockCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(stockDetails));

            var stock_Details = new CreateStockCommand() { CompanyCode = "C005", StockPrice=45.66, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = await controller.add(stock_Details);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        /// <summary>
        /// To verify Add with in valid data and returns BadRequest result
        /// </summary>
        [Fact]
        public async void Task_Add_InvalidStockData_Return_BadRequest()
        {
            //Arrange  
            string stockDetails = "";
            StockDetails stock = new StockDetails();
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.GetCompanyStockPrice(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<StockDetails>(stock));
            _mediator.Setup(m => m.Send(It.IsAny<CreateStockCommand>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(stockDetails));

            var stock_Details = new CreateStockCommand() { CompanyCode = "C005", StockPrice = 45.66, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act              
            var data = await controller.add(stock_Details);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        #endregion
    }
}
