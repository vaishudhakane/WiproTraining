using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnlineBookStore.Models.Validation;

public class IsbnAttribute : ValidationAttribute
{
    private static readonly Regex Isbn10or13 = new(@"^(97(8|9))?\d{9}(\d|X)$", RegexOptions.Compiled);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success!;
        var s = value.ToString()!.Replace("-", "").ToUpperInvariant();
        if (!Isbn10or13.IsMatch(s))
            return new ValidationResult("ISBN must be a valid ISBN-10 or ISBN-13.");
        return ValidationResult.Success!;
    }
}
