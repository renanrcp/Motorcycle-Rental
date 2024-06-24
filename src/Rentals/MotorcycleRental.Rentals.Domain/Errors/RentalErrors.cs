using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Rentals.Domain.Errors;

public static class RentalErrors
{
    public static readonly Error RentalTypeUnavailable = new("Rental.RentalType.Unavailable", "O tipo de locação está indisponível.");

    public static readonly Error RentalTypeNotExists = new("Rental.RentalType.Unavailable", "O tipo de locação não existe para essa quantidade de dias.");

    public static readonly Error EndDateLowerThanStartDate = new("Rental.EndDate.Lower", "A data de fim não pode ser menor que a data de início.");
}
