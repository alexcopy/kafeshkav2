using KafeshkaV2.DAL.Model;
namespace KafeshkaV2.BL.validators.payment;
using FluentValidation;

public class PaymentDetailValidator : AbstractValidator<PaymentDetail>
{
    public PaymentDetailValidator()
    {
        RuleFor(x => x.CardOwnerName)
            .NotEmpty().WithMessage("Card owner name is required")
            .MaximumLength(100).WithMessage("Card owner name cannot exceed 100 characters");

        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required")
            .Matches(@"^\d+$").WithMessage("Card number must contain only numbers")
            .Length(16).WithMessage("Card number must be 16 digits");

        RuleFor(x => x.ExpirationDate)
            .NotEmpty().WithMessage("Expiration date is required")
            .Matches(@"^\d{2}\/\d{2}$").WithMessage("Invalid expiration date format. Use MM/YY format");

        RuleFor(x => x.SecurityCode)
            .NotEmpty().WithMessage("Security code is required")
            .Matches(@"^\d+$").WithMessage("Security code must contain only numbers")
            .Length(3).WithMessage("Security code must be 3 digits");
    }
}