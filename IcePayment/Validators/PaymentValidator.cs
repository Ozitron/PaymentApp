using FluentValidation;
using IcePayment.API.Dto;

namespace IcePayment.API.Validators
{
    public class PaymentValidator : AbstractValidator<PaymentCreateDto>
    {
        public PaymentValidator()
        {
            RuleFor(a => a.Amount)
                .NotNull().WithMessage("{PropertyName} must not be null.")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");

            RuleFor(a => a.CurrencyCode)
                .NotNull().WithMessage("{PropertyName} must not be null.")
                .Length(3).WithMessage("{PropertyName} length must be 3 characters long.");

            RuleFor(a => a.Order)
                .NotNull().WithMessage("{PropertyName} must not be null.");

            RuleFor(a => a.Order.ConsumerFullName)
                .NotNull().WithMessage("{PropertyName} must not be null.")
                .NotEmpty().WithMessage("{PropertyName} must not be empty.")
                .MinimumLength(3).WithMessage("{PropertyName} must be longer than 3 characters.")
                .MaximumLength(80).WithMessage("{PropertyName} must be shorter than 80 characters.");

            RuleFor(a => a.Order.ConsumerAddress)
                .NotNull().WithMessage("{PropertyName} must be not null.")
                .NotEmpty().WithMessage("{PropertyName} must be not empty.")
                .MinimumLength(5).WithMessage("{PropertyName} must be longer than 5 characters.")
                .MaximumLength(250).WithMessage("{PropertyName} must be shorter than 250 characters.");
        }
    }
}
