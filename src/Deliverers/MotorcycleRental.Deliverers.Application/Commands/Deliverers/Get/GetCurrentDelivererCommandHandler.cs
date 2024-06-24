using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Core.Application.Abstractions;
using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Deliverers.Infrastructure.Contexts;
using MotorcycleRental.Deliverers.Infrastructure.Queries;

namespace MotorcycleRental.Deliverers.Application.Commands.Deliverers.Get;

public class GetCurrentDelivererCommandHandler(DeliverersDbContext dbContext) : ICommandHandler<GetCurrentDelivererCommand, GetCurrentDelivererResponse>
{
    private readonly DeliverersDbContext _dbContext = dbContext;

    public async Task<Result<GetCurrentDelivererResponse>> Handle(GetCurrentDelivererCommand request, CancellationToken cancellationToken)
    {
        var deliverer = await _dbContext.Deliverers
                                    .AsNoTracking()
                                    .WhereId(request.DelivererId)
                                    .FirstOrDefaultAsync(cancellationToken);

        if (deliverer == null)
        {
            return GetCurrentDelivererErrors.DelivererNotFound;
        }

        return new GetCurrentDelivererResponse
        {
            Id = deliverer.Id,
            BirthDate = deliverer.BirthDate,
            Cnh = deliverer.Cnh,
            CnhType = deliverer.CnhType,
            Cnpj = deliverer.Cnpj,
            Name = deliverer.Name,
        };
    }
}
