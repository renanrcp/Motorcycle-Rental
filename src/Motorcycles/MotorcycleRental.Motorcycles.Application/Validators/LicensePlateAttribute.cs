using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MotorcycleRental.Motorcycles.Application.Validators;

[AttributeUsage(AttributeTargets.Property)]
public partial class LicensePlateAttribute : ValidationAttribute
{
    [GeneratedRegex("^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$")]
    private static partial Regex LICENSE_PLATE_REGEX();

    public override bool IsValid(object? value)
    {
        return value is string str
        && !string.IsNullOrWhiteSpace(str)
        && LICENSE_PLATE_REGEX().IsMatch(str);
    }
}
