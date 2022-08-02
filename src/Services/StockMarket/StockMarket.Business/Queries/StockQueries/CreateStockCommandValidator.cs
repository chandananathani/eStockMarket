using FluentValidation;

namespace StockMarket.Business.Queries.CompanyQueries
{
    /// <summary>
    /// class is for create stock command validator
    /// </summary>
    public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
    {
        /// <summary>
        /// constructor for CreateStockCommandValidator 
        /// </summary>
        public CreateStockCommandValidator()
        {
            RuleFor(p => p.StockPrice)
               .NotEmpty().WithMessage("{StockPrice} is required.")
                .GreaterThan(0).WithMessage("{StockPrice} should be greater than zero.");

        }
    }
}
