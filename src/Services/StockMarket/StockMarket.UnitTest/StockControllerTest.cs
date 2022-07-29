using Microsoft.AspNetCore.Mvc;
using StockMarket.API.Controllers;
using StockMarket.Business.Queries.CompanyQueries;
using StockMarket.Business.StockBusiness;
using StockMarket.Model.StockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockMarket.UnitTest
{
    public class StockControllerTest
    {
        private IStockRepository _repository;

        public StockControllerTest(IStockRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        #region Get By Company Id, start date, end date 

        [Fact]
        public async void Task_GetStockById_Return_OkResult()
        {
            //Arrange  

            var controller = new StockController(_repository);
            var companyId = "C001";
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today.AddDays(3);
            //Act  
            var data = await controller.get(companyId,startDate.ToString(),endDate.ToString());

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetStockById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new StockController(_repository);
            var companyId = "C0023";
            var startDate = DateTime.Today.AddDays(-3);
            var endDate = DateTime.Today.AddDays(3);

            //Act  
            var data = await controller.get(companyId, startDate.ToString(), endDate.ToString());

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetStockById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new StockController(_repository);
            string companyId = null;
            string startDate = null;
            string endDate = null;

            //Act  
            var data = await controller.get(companyId,startDate,endDate);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        #endregion

        #region Create Stocks
        [Fact]
        public async void Task_Add_ValidStockData_Return_OkResult()
        {
            //Arrange  
            var controller = new StockController(_repository);
            var stock = new CreateStockCommand() { CompanyCode = "C005", StockPrice=45.66, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act  
            var data = await controller.add(stock);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_Add_InvalidStockData_Return_BadRequest()
        {
            //Arrange  
            var controller = new StockController(_repository);
            var stock = new CreateStockCommand() { CompanyCode = "C005", StockPrice = 45.66, CreatedBy = "chandana.natani@cognizant.com", CreatedDate = DateTime.Now };

            //Act              
            var data = await controller.add(stock);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
        #endregion
    }
}
