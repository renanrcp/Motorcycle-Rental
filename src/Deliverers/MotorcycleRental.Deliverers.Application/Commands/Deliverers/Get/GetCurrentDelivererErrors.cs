using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Get;

public static class GetCurrentDelivererErrors
{
    public static readonly NotFoundError DelivererNotFound = new("GetCurrentDeliverer.DelivererId.NotFound", "Não foi possível encontrar um entregador com esse id.");
}
