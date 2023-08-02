using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispatch_Controller_RESTapi.Migrations
{
    /// <inheritdoc />
    public partial class correct_cast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_DroneModel_ModelId",
                table: "Drones");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Drones",
                newName: "DroneModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Drones_ModelId",
                table: "Drones",
                newName: "IX_Drones_DroneModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_DroneModel_DroneModelId",
                table: "Drones",
                column: "DroneModelId",
                principalTable: "DroneModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_DroneModel_DroneModelId",
                table: "Drones");

            migrationBuilder.RenameColumn(
                name: "DroneModelId",
                table: "Drones",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Drones_DroneModelId",
                table: "Drones",
                newName: "IX_Drones_ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_DroneModel_ModelId",
                table: "Drones",
                column: "ModelId",
                principalTable: "DroneModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
