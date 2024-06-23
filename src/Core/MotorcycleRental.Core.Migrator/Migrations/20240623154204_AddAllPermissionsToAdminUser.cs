using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class AddAllPermissionsToAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                column: "name",
                values:
                [
                    "Admin"
                ]);

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: ["role_name", "permission_name"],
                values: new object[,] {
                    { "Admin", "All" }
                });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: ["user_id", "role_name"],
                values: new object[,] {
                        { 1, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_role",
                keyColumn: "role_name",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "role_permission",
                keyColumn: "role_name",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "name",
                keyValue: "Admin");
        }
    }
}
