using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StockMarket.Business.CompanyBusiness;
using StockMarket.Model.CompanyModel;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class CompanyController:ControllerBase
    {
        private readonly ICompanyRepository _repository;
        private readonly ILogger<CompanyController> _logger;
        public CompanyController(ICompanyRepository repository, ILogger<CompanyController>logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Company>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Company>>> getall()
        {
            var companys = await _repository.GetAllCompanys();
            return Ok(companys);
        }

        [HttpGet("{CompanyCode}")]
        //[HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Company>> info(string CompanyCode)
        {
            var companys = await _repository.GetCompanybyCode(CompanyCode);
            if (companys == null)
            {
                _logger.LogError($"Company with Company Code:{CompanyCode}, not found");
            }
            return Ok(companys);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Company>> register([FromBody] Company company)
        {
            await _repository.RegisterCompany(company);
            // return CreatedAtRoute("info", new { id = company.CompanyCode }, company);
            return Ok();

        }

        [HttpDelete("{CompanyCode}")]        
        [ProducesResponseType(typeof(Company), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> delete(string CompanyCode)
        {
           return Ok(await _repository.DeleteCompany(CompanyCode));           

        }

    }

}
