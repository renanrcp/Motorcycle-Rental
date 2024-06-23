using MotorcycleRental.Core.Domain.Primitives;
using MotorcycleRental.Motorcycles.Domain.Entities;

namespace MotorcycleRental.Motorcycles.Domain.Events;

public record MotorcycleCreatedEvent : DomainEvent
{
    private readonly Motorcycle _motorcycle;
    private int? _id;
    private int? _year;
    private string? _licensePlate;

    public MotorcycleCreatedEvent(Motorcycle motorcycle)
    {
        _motorcycle = motorcycle;
    }

    public int MotorcycleId
    {
        get
        {
            if (_id is 0 or null)
            {
                return _motorcycle.Id;
            }

            return _id.Value;
        }
        private set => _id = value;
    }

    public string LicensePlate
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(_licensePlate))
            {
                return _licensePlate;
            }

            return _motorcycle.LicensePlate;
        }
        private set => _licensePlate = value;
    }

    public int Year
    {
        get
        {
            if (_year is 0 or null)
            {
                return _motorcycle.Year;
            }

            return _year.Value;
        }
        private set => _year = value;
    }
}
