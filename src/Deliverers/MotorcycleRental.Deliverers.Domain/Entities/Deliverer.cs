using MotorcycleRental.Core.Domain.Abstractions;
using MotorcycleRental.Core.Domain.Entities;
using MotorcycleRental.Deliverers.Domain.Events;

namespace MotorcycleRental.Deliverers.Domain.Entities;

public class Deliverer : Entity
{
    private readonly List<DelivererImage> _images = [];

    private Deliverer(int id, string cnpj, string cnh, CnhType cnhType, string name, DateOnly birthDate)
    {
        Id = id;
        Cnpj = cnpj;
        Cnh = cnh;
        CnhType = cnhType;
        Name = name;
        BirthDate = birthDate;

        Raise(new DelivererCreatedEvent
        {
            DelivererId = id,
        });
    }

    public string Cnpj { get; private set; }

    public string Cnh { get; private set; }

    public CnhType CnhType { get; private set; }

    public string Name { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public IReadOnlyList<DelivererImage> Images => _images;

    public Result AddImage(DelivererImage image)
    {
        _images.Add(image);

        return Result.Success;
    }

    public static Result<Deliverer> Create(int id, string cnpj, string cnh, CnhType cnhType, string name, DateOnly birthDate)
    {
        var deliverer = new Deliverer(id, cnpj, cnh, cnhType, name, birthDate);

        return deliverer;
    }
}
