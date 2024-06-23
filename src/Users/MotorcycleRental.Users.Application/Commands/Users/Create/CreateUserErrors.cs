using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Users.Application.Commands.Users.Create;

public static class CreateUserErrors
{
    public static UnprocessableEntityError EmailExists = new("CreateUser.Email.Duplicated", "Já existe um usuário com esse e-mail.");
}
