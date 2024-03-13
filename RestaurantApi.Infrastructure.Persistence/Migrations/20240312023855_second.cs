using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ordenes_Mesas_MesaId1",
                table: "Ordenes");

            migrationBuilder.DropIndex(
                name: "IX_Ordenes_MesaId1",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "MesaId1",
                table: "Ordenes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MesaId1",
                table: "Ordenes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_MesaId1",
                table: "Ordenes",
                column: "MesaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ordenes_Mesas_MesaId1",
                table: "Ordenes",
                column: "MesaId1",
                principalTable: "Mesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
