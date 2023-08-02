using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispatch_Controller_RESTapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdDroneTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DroneId",
                table: "Drones",
                newName: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Drones",
                newName: "DroneId");
        }
    }
}
