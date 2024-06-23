using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Deliverers.Domain.Entities;

namespace MotorcycleRental.Deliverers.Infrastructure.Configurations;

public class DelivererImageEntityTypeConfiguration : IEntityTypeConfiguration<DelivererImage>
{
    public void Configure(EntityTypeBuilder<DelivererImage> builder)
    {
        builder.HasKey(x => new
        {
            x.DelivererId,
            x.Path,
        });
    }
}
