using MotorcycleRental.Core.Application.Abstractions;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Get;

public class GetCurrentDelivererCommand : ICommand<GetCurrentDelivererResponse>
{
    public required int DelivererId { get; set; }
}
