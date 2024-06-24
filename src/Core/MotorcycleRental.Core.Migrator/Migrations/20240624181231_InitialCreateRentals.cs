using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorcycleRental.Core.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateRentals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rental_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    days = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rental_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    motorcycle_id = table.Column<int>(type: "integer", nullable: false),
                    deliverer_id = table.Column<int>(type: "integer", nullable: false),
                    rental_type_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expected_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rentals", x => x.id);
                    table.ForeignKey(
                        name: "fk_rentals_deliverers_deliverer_id",
                        column: x => x.deliverer_id,
                        principalTable: "deliverers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rentals_motorcycles_motorcycle_id",
                        column: x => x.motorcycle_id,
                        principalTable: "motorcycles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rentals_rental_types_rental_type_id",
                        column: x => x.rental_type_id,
                        principalTable: "rental_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "rental_types",
                columns: new[] { "id", "cost", "days" },
                values: new object[,]
                {
                    { 1, 30m, 7 },
                    { 2, 28m, 15 },
                    { 3, 22m, 30 },
                    { 4, 20m, 45 },
                    { 5, 18m, 50 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_rentals_deliverer_id",
                table: "rentals",
                column: "deliverer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rentals_motorcycle_id",
                table: "rentals",
                column: "motorcycle_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rentals_rental_type_id",
                table: "rentals",
                column: "rental_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rentals");

            migrationBuilder.DropTable(
                name: "rental_types");
        }
    }
}
