using FluentValidation;
using IntusWindowsAPI.BL.Data;
using IntusWindowsAPI.BL.Dto;

namespace IntusWindowsAPI.Validation;

public class ProductDtoValidation : AbstractValidator<ProductDto>
{
    public ProductDtoValidation()
    {
        RuleFor(p => p.Type).Must(p => p == ProductType.Window || p == ProductType.Door)
            .WithMessage("Type must be either Window or Door.");
    }
}