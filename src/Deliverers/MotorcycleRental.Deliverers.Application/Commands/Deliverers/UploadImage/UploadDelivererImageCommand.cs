using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Deliverers.Application.Validators;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.UploadImage;

public class UploadDelivererImageCommand : ICommand
{
    [JsonIgnore]
    public int DelivererId { get; set; }

    [FromForm(Name = "image")]
    [Image]
    public required IFormFile Image { get; init; }
}
