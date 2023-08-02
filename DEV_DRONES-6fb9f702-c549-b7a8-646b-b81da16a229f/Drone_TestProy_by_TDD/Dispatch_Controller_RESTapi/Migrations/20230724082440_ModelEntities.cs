using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispatch_Controller_RESTapi.Migrations
{
    /// <inheritdoc />
    public partial class ModelEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_DroneModel_ModelId",
                table: "Drones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DroneModel",
                table: "DroneModel");

            migrationBuilder.RenameTable(
                name: "DroneModel",
                newName: "ModelEntities");

            migrationBuilder.RenameColumn(
                name: "Weigth",
                table: "Drones",
                newName: "WeigthLimit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModelEntities",
                table: "ModelEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drones_ModelEntities_ModelId",
                table: "Drones",
                column: "ModelId",
                principalTable: "ModelEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drones_ModelEntities_ModelId",
                table: "Drones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModelEntities",
                table: "ModelEntities");

            migrationBuilder.RenameTable(
                name: "ModelEntities",
                newName: "DroneModel");

            migrationBuilder.RenameColumn(
                name: "WeigthLimit",
                table: "Drones",
                newName: "Weigth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DroneModel",
                table: "DroneModel",
                column: "Id");

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
