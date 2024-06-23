﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotorcycleRental.Core.Migrator.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    [Migration("20240623154204_AddAllPermissionsToAdminUser")]
    partial class AddAllPermissionsToAdminUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MotorcycleRental.Motorcycles.Domain.Entities.Motorcycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("license_plate");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_motorcycles");

                    b.HasIndex("LicensePlate")
                        .IsUnique()
                        .HasDatabaseName("ix_motorcycles_license_plate");

                    b.ToTable("motorcycles", (string)null);
                });

            modelBuilder.Entity("MotorcycleRental.Users.Domain.Entities.Permission", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_permissions");

                    b.ToTable("permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Name = "All"
                        },
                        new
                        {
                            Name = "CanListMotorcycles"
                        },
                        new
                        {
                            Name = "CanCreateMotorcycle"
                        },
                        new
                        {
                            Name = "CanUpdateMotorcycles"
                        });
                });

            modelBuilder.Entity("MotorcycleRental.Users.Domain.Entities.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Name")
                        .HasName("pk_role");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("MotorcycleRental.Users.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_user_email");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("role_permission", b =>
                {
                    b.Property<string>("role_name")
                        .HasColumnType("text")
                        .HasColumnName("role_name");

                    b.Property<string>("permission_name")
                        .HasColumnType("text")
                        .HasColumnName("permission_name");

                    b.HasKey("role_name", "permission_name")
                        .HasName("pk_role_permission");

                    b.HasIndex("permission_name")
                        .HasDatabaseName("ix_role_permission_permission_name");

                    b.ToTable("role_permission", (string)null);
                });

            modelBuilder.Entity("user_role", b =>
                {
                    b.Property<int>("user_id")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("role_name")
                        .HasColumnType("text")
                        .HasColumnName("role_name");

                    b.HasKey("user_id", "role_name")
                        .HasName("pk_user_role");

                    b.HasIndex("role_name")
                        .HasDatabaseName("ix_user_role_role_name");

                    b.ToTable("user_role", (string)null);
                });

            modelBuilder.Entity("role_permission", b =>
                {
                    b.HasOne("MotorcycleRental.Users.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("permission_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permission_permissions_permission_name");

                    b.HasOne("MotorcycleRental.Users.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("role_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_permission_role_role_name");
                });

            modelBuilder.Entity("user_role", b =>
                {
                    b.HasOne("MotorcycleRental.Users.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("role_name")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_role_name");

                    b.HasOne("MotorcycleRental.Users.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_user_user_id");
                });
#pragma warning restore 612, 618
        }
    }
}
