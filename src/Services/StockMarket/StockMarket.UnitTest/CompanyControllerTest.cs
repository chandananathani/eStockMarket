using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic.CompilerServices;
using Moq;
using StockMarket.API;
using StockMarket.API.Common;
using StockMarket.API.Controllers;
using StockMarket.Business.Common;
using StockMarket.Business.CompanyBusiness;
using StockMarket.Model.Common;
using StockMarket.Model.CompanyModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockMarket.UnitTest
{
    public class CompanyControllerTest
    {
        readonly Mock<ICompanyRepository> _repository;
        readonly Mock<ILogger<CompanyController>> _logger;
        readonly Mock<IConfiguration> _configuration;
        readonly Mock<ITokenService> _tokenService;
        readonly CompanyController controller;        

        public CompanyControllerTest()
        {
             _repository = new Mock<ICompanyRepository>();
             _logger = new Mock<ILogger<CompanyController>>();
             _configuration = new Mock<IConfiguration>();
             _tokenService = new Mock<ITokenService>();
             controller = new CompanyController(_repository.Object, _logger.Object, _configuration.Object, _tokenService.Object);            
        }

        
        #region Get All Companys

        [Fact]
        public async void Task_GetCompanys_Return_OkResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company()
           };
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            //Act  
            var data = await controller.getall();
           
            //Assert             
            Assert.IsType<OkObjectResult>(data.Result);            
        }

        [Fact]
        public void Task_GetCompanys_Return_NotFoundResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>();
          
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            //Act  
            var data = controller.getall();

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result.Result);
        }

        [Fact]
        public void Task_GetCompanys_Return_BadRequestResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company()
           };
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            //Act  
            var data = controller.getall();
            
            //Assert  
            Assert.IsType<BadRequestObjectResult>(data.Result.Result);
        }
        #endregion

        #region Get By Company Id  

        [Fact]
        public void Task_GetCompanyById_Return_OkResult()
        {
            //Arrange  
            Company companies = new Company() {CompanyCode="C001" };
            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<Company>>(mock => mock.GetCompanybyCode(It.IsAny<string>())).Returns(Task.FromResult<Company>(companies));

            var companyId = "C001";

             //Act  
            var data = controller.info(companyId);
           
            //Assert             
            Assert.IsType<OkObjectResult>(data.Result.Result);    
        }

        [Fact]
        public void Task_GetCompanyById_Return_NotFoundResult()
        {
            //Arrange  
            Company companies = null;

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<Company>>(mock => mock.GetCompanybyCode(It.IsAny<string>())).Returns(Task.FromResult<Company>(companies));

            var companyId = "C0023";

            //Act  
            var data = controller.info(companyId);
          
            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result.Result);
        }

        [Fact]
        public void Task_GetCompanyById_Return_BadRequestResult()
        {
            //Arrange  
            Company companies = new Company() { CompanyCode = "C001" };

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<Company>>(mock => mock.GetCompanybyCode(It.IsAny<string>())).Returns(Task.FromResult<Company>(companies));

            var companyId = "C001";

            //Act  
            var data = controller.info(companyId);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data.Result.Result);
        }

        #endregion

        #region Create  Companys
        [Fact]
        public void Task_Add_ValidCompanyData_Return_OkResult()
        {
            //Arrange  
            Company companies = new Company();

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.RegisterCompany(It.IsAny<Company>())).Returns(Task.FromResult<Company>(companies));

            var company = new Company() { CompanyCode = "C005", CompanyName = "Company5", CompanyCEO = "CEO1",CompanyWebsite="www.Company5.com",CompanyTurnover=60000000,CreatedBy="chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = controller.register(company);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result.Result);
        }

        [Fact]
        public void Task_Add_InvalidCompany_Return_BadRequest()
        {
            //Arrange  
            Company companies = new Company();

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.RegisterCompany(It.IsAny<Company>())).Returns(Task.FromResult<Company>(companies));

            var company = new Company() { CompanyCode = "C005", CompanyName = "Company5", CompanyCEO = "CEO1", CompanyWebsite = "www.Company5.com", CompanyTurnover = 60000000, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = controller.register(company);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data.Result.Result);
        }
        #endregion

        #region Delete Companys  

        [Fact]
        public void Task_DeleteCompany_Return_OkResult()
        {
            //Arrange  
            Company companies = new Company() { CompanyCode = "C001" };

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<Company>>(mock => mock.GetCompanybyCode(It.IsAny<string>())).Returns(Task.FromResult<Company>(companies));
            _repository.Setup(mock => mock.DeleteCompany(It.IsAny<string>()));

            var companyId = "C001";

            //Act  
            var data = controller.delete(companyId);

            //Assert  
            Assert.IsType<OkObjectResult>(data.Result);
        }

        [Fact]
        public void Task_DeleteCompany_Return_NotFoundResult()
        {
            //Arrange  
            Company companies = new Company();

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjMzMjI1OCIsIkZpcnN0TmFtZSI6IkNoYW5kYW5hIiwiTGFzdE5hbWUiOiJOYXRhbmkiLCJVc2VyTmFtZSI6Im5hdGFuaWMiLCJFbWFpbCI6ImNoYW5kYW5hLm5hdGFuaUBjb2duaXphbnQuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJlZGNkMDVmNC1mOTVkLTQ1MTQtYTJjYi0zOTZhYTFkOTg3NmIiLCJleHAiOjE2NTkxMDQxMjYsImlzcyI6IlN0b2NrbWFya2V0QXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJTdG9ja21hcmtldEF1dGhlbnRpY2F0aW9uU2VydmVyIn0.A69lGRWp1_w5MH9OjB8-RXPThP5mDlhAhafaeZ9wHGg";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.DeleteCompany(It.IsAny<string>()));

            var companyId = "C0026";

            //Act  
            var data = controller.delete(companyId);

            //Assert  
           Assert.IsType<NotFoundObjectResult>(data.Result);
        }

        [Fact]
        public void Task_DeleteCompany_Return_BadRequestResult()
        {
            //Arrange  
            Company companies = new Company() { CompanyCode = "C001" };

            string COOKIE_NAME = "Token";
            string COOKIE_VALUE = "";
            var cookie = new StringValues(COOKIE_NAME + "=" + COOKIE_VALUE);
            controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            controller.ControllerContext.HttpContext.Request.Headers.Add(HeaderNames.Cookie, cookie);

            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf");
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("InventoryAuthenticationServer");
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup(mock => mock.DeleteCompany(It.IsAny<string>()));

            var companyId = "C001";

            //Act  
            var data = controller.delete(companyId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        #endregion

    }
}
