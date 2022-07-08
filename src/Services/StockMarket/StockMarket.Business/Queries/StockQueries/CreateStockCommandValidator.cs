using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class CreateStockCommandValidator: AbstractValidator<CreateStockCommand>
    {
        public CreateStockCommandValidator()
        {
            RuleFor(p => p.StockPrice)
               .NotEmpty().WithMessage("{StockPrice} is required.")
                .GreaterThan(0).WithMessage("{StockPrice} should be greater than zero.");

        }
    }
}
