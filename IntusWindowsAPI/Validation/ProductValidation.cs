using FluentValidation;
using IntusWindowsAPI.BL.Data;

namespace IntusWindowsAPI.Validation;

public class ProductValidation : AbstractValidator<Product>
{
    public ProductValidation()
    {
        RuleFor(p => p.Type)
            .IsInEnum();
        RuleFor(p => p.Id)
            .GreaterThanOrEqualTo(0);
    }
}