using System.ComponentModel.DataAnnotations;

namespace OnlineBookStore.Models.Validation;

public class PriceRangeAttribute : ValidationAttribute
{
    public decimal Min { get; }
    public decimal Max { get; }

    public PriceRangeAttribute(double min, double max)
    {
        Min = (decimal)min;
        Max = (decimal)max;
        ErrorMessage = $"Price must be between {Min} and {Max}.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult(ErrorMessage);
        var price = (decimal)value;
        if (price < Min || price > Max)
            return new ValidationResult(ErrorMessage);
        return ValidationResult.Success!;
    }
}
