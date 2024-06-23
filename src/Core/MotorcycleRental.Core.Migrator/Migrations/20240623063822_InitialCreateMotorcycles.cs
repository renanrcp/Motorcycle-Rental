using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMotorcycles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "motorcycles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    license_plate = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_motorcycles", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "permissions",
                column: "name",
                values: new object[]
                {
                    "CanCreateMotorcycle",
                    "CanListMotorcycles",
                    "CanUpdateMotorcycles"
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: ["id", "email", "name", "password"],
                values: new object[,] {
                    { 1, "admin@motorcyclerental.com", "Admin", "$2a$11$ecW4gFhBg0IDN9XBiMcTRuzWNeYCRmkMjG/HFq27QSJVAtCEsPAk2" }
                });


            migrationBuilder.CreateIndex(
                name: "ix_motorcycles_license_plate",
                table: "motorcycles",
                column: "license_plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "motorcycles");

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "name",
                keyValue: "CanCreateMotorcycle");

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "name",
                keyValue: "CanListMotorcycles");

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "name",
                keyValue: "CanUpdateMotorcycles");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
