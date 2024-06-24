using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Deliverers.Application.Abstractions.Users;

public interface IUsersService
{
    Task<Result<CreateUserResponse>> CreateUserAsync(CreateUserRequest request);
}
