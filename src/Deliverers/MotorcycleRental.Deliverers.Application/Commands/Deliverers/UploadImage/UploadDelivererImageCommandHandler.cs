using Microsoft.EntityFrameworkCore;
using Microsoft.IO;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Deliverers.Domain.Entities;
using MotorcycleRental.Deliverers.Infrastructure.Contexts;
using MotorcycleRental.Deliverers.Infrastructure.Queries;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.UploadImage;

public class UploadDelivererImageCommandHandler(DeliverersDbContext dbContext) : ICommandHandler<UploadDelivererImageCommand>
{
    private readonly DeliverersDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UploadDelivererImageCommand request, CancellationToken cancellationToken)
    {
        var deliverer = await _dbContext.Deliverers
                                .Include(x => x.Images)
                                .AsTracking()
                                .WhereId(request.DelivererId)
                                .FirstOrDefaultAsync(cancellationToken);

        if (deliverer == null)
        {
            return UploadDelivererImageErrors.DelivererNotFound;
        }

        var directoryPath = Path.Join(Directory.GetCurrentDirectory(), "images");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Join(directoryPath, $"{Guid.NewGuid()}_{request.Image.FileName}");

        var imageResult = DelivererImage.Create(request.DelivererId, filePath);

        if (imageResult.IsFaulted)
        {
            return imageResult.Error!;
        }

        var delivererImage = imageResult.Value!;

        var imageAddResult = deliverer.AddImage(delivererImage);

        if (imageAddResult.IsFaulted)
        {
            return imageAddResult.Error!;
        }

        using var fileStream = File.Create(filePath);

        await request.Image.CopyToAsync(fileStream, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
