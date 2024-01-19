using FluentValidation;
using IntusWindowsAPI.BL.Dto;

namespace IntusWindowsAPI.Validation;

public class UpdateOrderDtoValidation : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderDtoValidation()
    {
        RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage($"Name is required");
        RuleFor(o => o.State)
            .NotEmpty()
            .WithMessage($"State is required");
    }
}