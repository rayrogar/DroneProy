using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispatch_Controller_RESTapi.Migrations
{
    /// <inheritdoc />
    public partial class SuprStateModelEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_DroneModel_DroneModelId",
                table: "Drones");

            migrationBuilder.DropForeignKey(
                name: "FK_Drones_States_StateId",
                table: "Drones");

            migrationBuilder.DropIndex(
                name: "IX_Drones_DroneModelId",
                table: "Drones");

            migrationBuilder.DropIndex(
                name: "IX_Drones_StateId",
                table: "Drones");

            migrationBuilder.DropColumn(
                name: "DroneModelId",
                table: "Drones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DroneModelId",
                table: "Drones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_DroneModelId",
                table: "Drones",
                column: "DroneModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Drones_StateId",
                table: "Drones",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_DroneModel_DroneModelId",
                table: "Drones",
                column: "DroneModelId",
                principalTable: "DroneModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_States_StateId",
                table: "Drones",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
