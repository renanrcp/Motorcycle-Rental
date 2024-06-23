using MotorcycleRental.Core.Application.Errors;

namespace MotorcycleRental.Users.Application.Commands.Users.Login;

public static class LoginUserErrors
{
    // É comum apenas informarmos no login que o usuário está fazendo algo de errado.
    // Evitando que um hacker descubra se esse e-mail existe na base por exemplo.
    public static readonly UnprocessableEntityError InvalidData = new("LoginUser.InvalidData", "Dados inválidos.");
}
