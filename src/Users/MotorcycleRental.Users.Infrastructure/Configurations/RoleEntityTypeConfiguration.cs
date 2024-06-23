using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Infrastructure.Configurations;

public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Name);

        builder.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles)
                .UsingEntity(
                    "role_permission",
                        l => l.HasOne(typeof(Permission)).WithMany().HasForeignKey("permission_name").HasPrincipalKey(nameof(Permission.Name)),
                        r => r.HasOne(typeof(Role)).WithMany().HasForeignKey("role_name").HasPrincipalKey(nameof(Role.Name)),
                        j =>
                        {
                            j.HasKey("role_name", "permission_name");
                            j.HasData([
                                new
                                {
                                    role_name = "Admin",
                                    permission_name = "All"
                                }
                            ]);
                        }
                );

        builder.HasData([new
        {
            Name = "Admin"
        }]);
    }
}
