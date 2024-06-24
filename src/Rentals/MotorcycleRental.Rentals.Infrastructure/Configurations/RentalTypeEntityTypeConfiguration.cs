using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Rentals.Domain.Entities;

namespace MotorcycleRental.Rentals.Infrastructure.Configurations;

public class RentalTypeEntityTypeConfiguration : IEntityTypeConfiguration<RentalType>
{
    public void Configure(EntityTypeBuilder<RentalType> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData([
            new
            {
                Id = 1,
                Days = 7,
                Cost = 30m,
            },
            new
            {
                Id = 2,
                Days = 15,
                Cost = 28m,
            },
            new
            {
                Id = 3,
                Days = 30,
                Cost = 22m,
            },
            new
            {
                Id = 4,
                Days = 45,
                Cost = 20m,
            },
            new
            {
                Id = 5,
                Days = 50,
                Cost = 18m,
            },
        ]);
    }
}
