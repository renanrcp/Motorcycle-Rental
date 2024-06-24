using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Configurations;

public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(x => x.Id);

        builder
           .HasOne(x => x.RentalType)
           .WithMany()
           .HasForeignKey(x => x.RentalTypeId)
           .IsRequired(true);
    }
}
