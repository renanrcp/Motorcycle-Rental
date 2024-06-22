using MediatR;
using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Core.Application.Abstractions;

public interface ICommand : IRequest<Result>
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
