using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Motorcycles.Domain.Entities;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Motorcycles.Infrastructure.Configurations;

public class MotorcycleEntityTypeConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.LicensePlate).IsUnique();

        builder
            .HasOne<Rental>()
            .WithOne()
            .HasForeignKey<Rental>(x => x.MotorcycleId)
            .IsRequired();
    }
}
