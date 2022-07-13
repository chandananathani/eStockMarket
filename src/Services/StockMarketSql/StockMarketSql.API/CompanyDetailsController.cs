﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockMarketSql.API.Common;
using StockMarketSql.Business;
using StockMarketSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockMarketSql.API
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class CompanyDetailsController : ControllerBase
    {
        private readonly ICompanyDetailsBusiness _repository;
        private readonly ILogger<CompanyDetailsController> _logger;
        public IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public CompanyDetailsController(ICompanyDetailsBusiness repository, ILogger<CompanyDetailsController> logger, IConfiguration config, ITokenService tokenService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CompanyDetails), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyDetails>> register([FromBody] CompanyDetails company)
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return BadRequest("Please Provide JWT token");
            }

            if (_tokenService.ValidateToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), "", token))
            {
                _repository.RegisterCompany(company);              
                return Ok("Company Details Created Sucessfully");
            }
            else
            {
                return BadRequest("Invalid JWT token");
            }

        }
    }
}
