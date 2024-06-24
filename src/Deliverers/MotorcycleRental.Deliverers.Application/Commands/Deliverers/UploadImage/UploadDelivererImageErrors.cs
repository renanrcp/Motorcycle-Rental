using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.UploadImage;

public static class UploadDelivererImageErrors
{
    public static readonly NotFoundError DelivererNotFound = new("UploadDelivererImage.DelivererId.NotFound", "Não foi possível encontrar um entregador com esse id.");
}
