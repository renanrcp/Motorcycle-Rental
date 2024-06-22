using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity(
                    "user_role",
                        l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("role_name").HasPrincipalKey(nameof(Role.Name)),
                        r => r.HasOne(typeof(User)).WithMany().HasForeignKey("user_id").HasPrincipalKey(nameof(User.Id)),
                        j => j.HasKey("user_id", "role_name")
                );
    }
}
