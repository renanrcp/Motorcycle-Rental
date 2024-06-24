﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MotorcycleRental.Core.Migrator.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    partial class MigrationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MotorcycleRental.Deliverers.Domain.Entities.Deliverer", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Cnh")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cnh");

                    b.Property<int>("CnhType")
                        .HasColumnType("integer")
                        .HasColumnName("cnh_type");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cnpj");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_deliverers");

                    b.ToTable("deliverers", (string)null);
                });

            modelBuilder.Entity("MotorcycleRental.Deliverers.Domain.Entities.DelivererImage", b =>
                {
                    b.Property<int>("DelivererId")
                        .HasColumnType("integer")
                        .HasColumnName("deliverer_id");

                    b.Property<string>("Path")
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.HasKey("DelivererId", "Path")
                        .HasName("pk_deliverer_images");

                    b.ToTable("deliverer_images", (string)null);
                });

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

            modelBuilder.Entity("MotorcycleRental.Rentals.Domain.Entities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DelivererId")
                        .HasColumnType("integer")
                        .HasColumnName("deliverer_id");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("ExpectedEndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expected_end_date");

                    b.Property<int>("MotorcycleId")
                        .HasColumnType("integer")
                        .HasColumnName("motorcycle_id");

                    b.Property<int>("RentalTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("rental_type_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id")
                        .HasName("pk_rentals");

                    b.HasIndex("DelivererId")
                        .IsUnique()
                        .HasDatabaseName("ix_rentals_deliverer_id");

                    b.HasIndex("MotorcycleId")
                        .IsUnique()
                        .HasDatabaseName("ix_rentals_motorcycle_id");

                    b.HasIndex("RentalTypeId")
                        .HasDatabaseName("ix_rentals_rental_type_id");

                    b.ToTable("rentals", (string)null);
                });

            modelBuilder.Entity("MotorcycleRental.Rentals.Domain.Entities.RentalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric")
                        .HasColumnName("cost");

                    b.Property<int>("Days")
                        .HasColumnType("integer")
                        .HasColumnName("days");

                    b.HasKey("Id")
                        .HasName("pk_rental_types");

                    b.ToTable("rental_types", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 30m,
                            Days = 7
                        },
                        new
                        {
                            Id = 2,
                            Cost = 28m,
                            Days = 15
                        },
                        new
                        {
                            Id = 3,
                            Cost = 22m,
                            Days = 30
                        },
                        new
                        {
                            Id = 4,
                            Cost = 20m,
                            Days = 45
                        },
                        new
                        {
                            Id = 5,
                            Cost = 18m,
                            Days = 50
                        });
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

                    b.HasData(
                        new
                        {
                            Name = "Admin"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@motorcyclerental.com",
                            Name = "Admin",
                            Password = "$2a$11$ecW4gFhBg0IDN9XBiMcTRuzWNeYCRmkMjG/HFq27QSJVAtCEsPAk2"
                        });
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

                    b.HasData(
                        new
                        {
                            role_name = "Admin",
                            permission_name = "All"
                        });
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

                    b.HasData(
                        new
                        {
                            user_id = 1,
                            role_name = "Admin"
                        });
                });

            modelBuilder.Entity("MotorcycleRental.Deliverers.Domain.Entities.Deliverer", b =>
                {
                    b.HasOne("MotorcycleRental.Users.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("MotorcycleRental.Deliverers.Domain.Entities.Deliverer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_deliverers_user_id");
                });

            modelBuilder.Entity("MotorcycleRental.Deliverers.Domain.Entities.DelivererImage", b =>
                {
                    b.HasOne("MotorcycleRental.Deliverers.Domain.Entities.Deliverer", null)
                        .WithMany("Images")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_deliverer_images_deliverers_deliverer_id");
                });

            modelBuilder.Entity("MotorcycleRental.Rentals.Domain.Entities.Rental", b =>
                {
                    b.HasOne("MotorcycleRental.Deliverers.Domain.Entities.Deliverer", null)
                        .WithOne()
                        .HasForeignKey("MotorcycleRental.Rentals.Domain.Entities.Rental", "DelivererId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_deliverers_deliverer_id");

                    b.HasOne("MotorcycleRental.Motorcycles.Domain.Entities.Motorcycle", null)
                        .WithOne()
                        .HasForeignKey("MotorcycleRental.Rentals.Domain.Entities.Rental", "MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_motorcycles_motorcycle_id");

                    b.HasOne("MotorcycleRental.Rentals.Domain.Entities.RentalType", "RentalType")
                        .WithMany()
                        .HasForeignKey("RentalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rentals_rental_types_rental_type_id");

                    b.Navigation("RentalType");
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

            modelBuilder.Entity("MotorcycleRental.Deliverers.Domain.Entities.Deliverer", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
