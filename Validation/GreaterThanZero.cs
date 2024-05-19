using System.ComponentModel.DataAnnotations;

namespace trade_compas.Validation;

public class GreaterThanZero : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var v = (double)value;
        return v > 0;
    }
}
