using BeerWholesaleManagementSystem.Core.DTO;
using FluentValidation;

namespace BeerWholesaleManagementSystem.Core.Validators
{
    /// <summary>
    /// Validator for QuoteResquestDto
    /// </summary>
    public class QuoteRequestValidator : AbstractValidator<QuoteResquestDto>
    {
        /// <summary>
        /// Initializes a new instance of the QuoteRequestValidator class.
        /// </summary>
        public QuoteRequestValidator()
        {
            RuleFor(command => command.WholesalerId)
                .NotEmpty().WithMessage("The WholesalerId is required.");
            RuleFor(command => command.RequestDate)
                .NotEmpty().WithMessage("The Quote Date field is mandatory.");
            RuleFor(command => command.QuoteBeerItemDto)
                .NotEmpty().WithMessage("The List of beers is required.");
        }
    }
}
