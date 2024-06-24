using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Core.Application.Abstractions;
using System.Text.Json.Serialization;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.UploadImage;

public class UploadDelivererImageCommand : ICommand
{
    [JsonIgnore]
    public int DelivererId { get; set; }

    [FromForm(Name = "image")]
    public required IFormFile Image { get; init; }
}
