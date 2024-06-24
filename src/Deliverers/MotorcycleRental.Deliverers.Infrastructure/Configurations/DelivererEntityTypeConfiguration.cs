using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Deliverers.Domain.Entities;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Deliverers.Infrastructure.Configurations;

public class DelivererEntityTypeConfiguration : IEntityTypeConfiguration<Deliverer>
{
    public void Configure(EntityTypeBuilder<Deliverer> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Images)
            .WithOne()
            .HasForeignKey(x => x.DelivererId);

        builder
            .HasOne<Rental>()
            .WithOne()
            .HasForeignKey<Rental>(x => x.DelivererId)
            .IsRequired();
    }
}
