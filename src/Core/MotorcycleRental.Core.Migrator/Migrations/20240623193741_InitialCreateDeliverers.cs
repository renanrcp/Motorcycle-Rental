using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleRental.Core.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDeliverers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliverers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    cnh = table.Column<string>(type: "text", nullable: false),
                    cnh_type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliverers", x => x.id);
                    table.ForeignKey(
                        name: "fk_deliverers_user_id",
                        column: x => x.id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliverer_images",
                columns: table => new
                {
                    deliverer_id = table.Column<int>(type: "integer", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliverer_images", x => new { x.deliverer_id, x.path });
                    table.ForeignKey(
                        name: "fk_deliverer_images_deliverers_deliverer_id",
                        column: x => x.deliverer_id,
                        principalTable: "deliverers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliverer_images");

            migrationBuilder.DropTable(
                name: "deliverers");
        }
    }
}
