using FluentValidation;
using IntusWindowsAPI.BL.Dto;

namespace IntusWindowsAPI.Validation;

public class OrderDtoValidation : AbstractValidator<OrderDto>
{
    public OrderDtoValidation()
    {
        RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage($"Name is required");
    }
}