using MotorcycleRental.Core.Domain.Abstractions;

namespace MotorcycleRental.Deliverers.Domain.Entities;

public class DelivererImage
{
    private DelivererImage(int delivererId, string path)
    {
        DelivererId = delivererId;
        Path = path;
    }

    public int DelivererId { get; private set; }

    public string Path { get; private set; }

    public static Result<DelivererImage> Create(int delivererId, string path)
    {
        var image = new DelivererImage(delivererId, path);

        return image;
    }
}
