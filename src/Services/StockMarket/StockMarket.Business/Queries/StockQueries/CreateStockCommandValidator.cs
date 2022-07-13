using FluentValidation;

namespace StockMarket.Business.Queries.CompanyQueries
{
    public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
    {
        public CreateStockCommandValidator()
        {
            RuleFor(p => p.StockPrice)
               .NotEmpty().WithMessage("{StockPrice} is required.")
                .GreaterThan(0).WithMessage("{StockPrice} should be greater than zero.");

        }
    }
}
