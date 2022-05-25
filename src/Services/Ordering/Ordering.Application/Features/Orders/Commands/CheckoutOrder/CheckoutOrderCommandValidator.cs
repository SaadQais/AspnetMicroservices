using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(order => order.UserName)
                .NotEmpty().WithMessage("{UserName} cannot be empty.")
                .NotNull().WithMessage("{UserName} cannot be null.")
                .MaximumLength(50).WithMessage("{UserName} cannot be longer than 50 characters.");

            RuleFor(order => order.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress} cannot be empty.");

            RuleFor(order => order.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} cannot be empty.")
                .GreaterThan(0).WithMessage("{TotalPrice} cannot be less than 0.");
        }
    }
}
