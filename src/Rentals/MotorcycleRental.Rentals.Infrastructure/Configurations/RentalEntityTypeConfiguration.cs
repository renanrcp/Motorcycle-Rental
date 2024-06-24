using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Configurations;

public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.MotorcycleId).IsUnique();
        builder.HasIndex(x => x.DelivererId).IsUnique();

        builder
            .Property(x => x.StartDate)
            .HasColumnType("timestamp without time zone");

        builder
            .Property(x => x.EndDate)
            .HasColumnType("timestamp without time zone");

        builder
            .Property(x => x.ExpectedEndDate)
            .HasColumnType("timestamp without time zone");

        builder
           .HasOne(x => x.RentalType)
           .WithMany()
           .HasForeignKey(x => x.RentalTypeId)
           .IsRequired(true);
    }
}
