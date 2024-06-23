using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Motorcycles.Domain.Events;

namespace MotorcycleRental.Motorcycles.Domain.Entities;

public class Motorcycle : Entity
{
    private Motorcycle(string licensePlate, int year, string model)
    {
        LicensePlate = licensePlate;
        Year = year;
        Model = model;

        Raise(new MotorcycleCreatedEvent
        {
            LicensePlate = licensePlate,
        });
    }

    public string LicensePlate { get; private set; }

    public int Year { get; private set; }

    public string Model { get; private set; }

    public static Result<Motorcycle> Create(string licensePlate, int year, string model)
    {
        var motorcycle = new Motorcycle(licensePlate, year, model);

        return motorcycle;
    }
}