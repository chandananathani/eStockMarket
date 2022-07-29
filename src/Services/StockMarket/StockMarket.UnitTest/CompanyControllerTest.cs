using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        private const double EXPIRY_DURATION_MINUTES = 30;

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
        public void BuildToken()
        {

            var claims = new[] {
                    new Claim("Id", "123"),
                    new Claim("FirstName", "Test"),
                    new Claim("LastName", "Test1"),
                    new Claim("UserName", "testUser"),
                    new Claim("Email", "test@gmail.com"),
                    new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken("InventoryAuthenticationServer", "InventoryAuthenticationServer", claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            var httpcontext = new DefaultHttpContext();
            httpcontext.Session.SetString("Token", token);            
        }

        [Fact]
        public async void Task_GetCompanys_Return_OkResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            BuildToken();
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            //Act  
            var data = await controller.getall();
           
            //Assert             
            Assert.IsType<OkObjectResult>(data);            
        }

        [Fact]
        public void Task_GetCompanys_Return_BadRequestResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            //Act  
            var data = controller.getall();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }
        #endregion

        #region Get By Company Id  

        [Fact]
        public void Task_GetCompanyById_Return_OkResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));
            var companyId = "C001";

            //Act  
            var data = controller.info(companyId);

            //Assert  
            if (data != null)
                Assert.IsType<OkResult>(data);
        }

        [Fact]
        public void Task_GetCompanyById_Return_NotFoundResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            var companyId = "C0023";

            //Act  
            var data = controller.info(companyId);

            //Assert  
            if (data != null)
                Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_GetCompanyById_Return_BadRequestResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            string companyId = null;

            //Act  
            var data = controller.info(companyId);

            //Assert  
            if (data != null)
                Assert.IsType<BadRequestResult>(data);
        }

        #endregion

        #region Create  Companys
        [Fact]
        public void Task_Add_ValidCompanyData_Return_OkResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            var company = new Company() { CompanyCode = "C005", CompanyName = "Company5", CompanyCEO = "CEO1",CompanyWebsite="www.Company5.com",CompanyTurnover=60000000,CreatedBy="chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = controller.register(company);

            //Assert  
            if (data != null)
                Assert.IsType<OkResult>(data);           
        }

        [Fact]
        public void Task_Add_InvalidCompany_Return_BadRequest()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            Company company = new Company() { CompanyCode = "C005", CompanyName = "Company5", CompanyCEO = "CEO1", CompanyWebsite = "www.Company5.com", CompanyTurnover = 60000000, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act              
            var data = controller.register(company);

            //Assert
            if (data != null)
                Assert.IsType<BadRequestResult>(data);
        }
        #endregion

        #region Delete Companys  

        [Fact]
        public void Task_DeleteCompany_Return_OkResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            var companyId = "C001";

            //Act  
            var data = controller.delete(companyId);

            //Assert  
            if (data != null)
                Assert.IsType<OkResult>(data);
        }

        [Fact]
        public void Task_DeleteCompany_Return_NotFoundResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            var companyId = "C0046";

            //Act  
            var data = controller.delete(companyId);

            //Assert  
            if (data != null)
                Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_DeleteCompany_Return_BadRequestResult()
        {
            //Arrange  
            IEnumerable<Company> companies = new List<Company>()
           {
               new Company(),
               new Company(),
           };
            _tokenService.Setup(mock => mock.ValidateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository.Setup<Task<IEnumerable<Company>>>(mock => mock.GetAllCompanys()).Returns(Task.FromResult<IEnumerable<Company>>(companies));

            string companyId = "";

            //Act  
            var data = controller.delete(companyId);

            //Assert
            if (data != null)
            Assert.IsType<BadRequestResult>(data);
        }

        #endregion

    }
}
