using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Infrastructure.Configurations;

public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Name);
    }
}
