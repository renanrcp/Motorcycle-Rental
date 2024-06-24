using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class AddCanDeleteMotorcyclePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "permissions",
                column: "name",
                value: "CanDeleteMotorcycles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "name",
                keyValue: "CanDeleteMotorcycles");
        }
    }
}
