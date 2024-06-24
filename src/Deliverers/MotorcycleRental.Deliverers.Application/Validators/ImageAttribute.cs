using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Deliverers.Application.Validators;

[AttributeUsage(AttributeTargets.Property)]
public class ImageAttribute : ValidationAttribute
{
    public ImageAttribute()
    {
        ErrorMessage = "The image format is invalid, valid formats are '.png' and '.bmp'.";
    }

    public override bool IsValid(object? value)
    {
        return value is IFormFile formFile and not null
        && (
            formFile.FileName.EndsWith(".png") ||
            formFile.FileName.EndsWith(".bmp")
        );
    }
}
