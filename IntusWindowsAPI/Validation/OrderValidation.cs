using FluentValidation;
using IntusWindowsAPI.BL.Data;

namespace IntusWindowsAPI.Validation;

public class OrderValidation : AbstractValidator<Order>
{
    public OrderValidation()
    {
        RuleFor(o => o.Id)
            .GreaterThanOrEqualTo(0);
    }
    
}