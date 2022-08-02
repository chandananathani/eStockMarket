using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Moq;
using StockMarketSql.API;
using StockMarketSql.API.Common;
using StockMarketSql.Business;
using StockMarketSql.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StockMarketSql.UnitTest
{
    /// <summary>
    /// Test class for <see cref="CompanyDetailsController"/>
    /// </summary>
    public class CompanyDetailsControllerTest
    {
        readonly Mock<ICompanyDetailsBusiness> _mockrepository;
        readonly Mock<ILogger<CompanyDetailsController>> _mocklogger;
        readonly Mock<IConfiguration> _mockconfiguration;
        readonly Mock<ITokenService> _mocktokenService;
        readonly CompanyDetailsController controller;

        /// <summary>
        /// constructor for CompanyDetailsControllerTest 
        /// </summary>
        public CompanyDetailsControllerTest()
        {
            _mockrepository = new Mock<ICompanyDetailsBusiness>();
            _mocklogger = new Mock<ILogger<CompanyDetailsController>>();
            _mockconfiguration = new Mock<IConfiguration>();
            _mocktokenService = new Mock<ITokenService>();
            controller = new CompanyDetailsController(_mockrepository.Object, _mocklogger.Object, _mockconfiguration.Object, _mocktokenService.Object);
        }


        #region Create  Companys

        /// <summary>
        /// To verify GetCompanys with valid data and returns OK result
        /// </summary>
        [Fact]
        public void Task_Add_ValidCompanyData_Return_OkResult()
        {
            //Arrange  
            CompanyDetails companies = new CompanyDetails();

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _mockconfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _mockconfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _mocktokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _mockrepository.Setup(mock => mock.RegisterCompany(It.IsAny<CompanyDetails>())).Returns(Task.FromResult<CompanyDetails>(companies));

            var company = new CompanyDetails() { CompanyCode = "C005", CompanyName = "Company5", CompanyCEO = "CEO1", CompanyWebsite = "www.Company5.com", CompanyTurnover = 60000000, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = controller.register(company);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result.Result);
        }

        /// <summary>
        /// To verify GetCompanys with in valid data and returns BadRequest result
        /// </summary>
        [Fact]
        public void Task_Add_InvalidCompany_Return_BadRequest()
        {
            //Arrange  
            CompanyDetails companies = new CompanyDetails();

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _mockconfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _mockconfiguration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _mocktokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _mockrepository.Setup(mock => mock.RegisterCompany(It.IsAny<CompanyDetails>())).Returns(Task.FromResult<CompanyDetails>(companies));

            var company = new CompanyDetails() { CompanyCode = "C005", CompanyName = "Company5", CompanyCEO = "CEO1", CompanyWebsite = "www.Company5.com", CompanyTurnover = 60000000, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = controller.register(company);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data.Result.Result);
        }
        #endregion
    }
}
