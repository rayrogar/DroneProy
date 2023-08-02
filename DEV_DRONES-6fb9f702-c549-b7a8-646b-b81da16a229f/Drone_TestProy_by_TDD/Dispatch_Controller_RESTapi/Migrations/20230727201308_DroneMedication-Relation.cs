using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dispatch_Controller_RESTapi.Migrations
{
    /// <inheritdoc />
    public partial class DroneMedicationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DroneMedicationsEntity_Drones_DroneId",
                table: "DroneMedicationsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DroneMedicationsEntity_Medications_MedicationId",
                table: "DroneMedicationsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DroneMedicationsEntity",
                table: "DroneMedicationsEntity");

            migrationBuilder.RenameTable(
                name: "DroneMedicationsEntity",
                newName: "DroneMedications");

            migrationBuilder.RenameIndex(
                name: "IX_DroneMedicationsEntity_MedicationId",
                table: "DroneMedications",
                newName: "IX_DroneMedications_MedicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DroneMedications",
                table: "DroneMedications",
                columns: new[] { "DroneId", "MedicationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DroneMedications_Drones_DroneId",
                table: "DroneMedications",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DroneMedications_Medications_MedicationId",
                table: "DroneMedications",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DroneMedications_Drones_DroneId",
                table: "DroneMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_DroneMedications_Medications_MedicationId",
                table: "DroneMedications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DroneMedications",
                table: "DroneMedications");

            migrationBuilder.RenameTable(
                name: "DroneMedications",
                newName: "DroneMedicationsEntity");

            migrationBuilder.RenameIndex(
                name: "IX_DroneMedications_MedicationId",
                table: "DroneMedicationsEntity",
                newName: "IX_DroneMedicationsEntity_MedicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DroneMedicationsEntity",
                table: "DroneMedicationsEntity",
                columns: new[] { "DroneId", "MedicationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DroneMedicationsEntity_Drones_DroneId",
                table: "DroneMedicationsEntity",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DroneMedicationsEntity_Medications_MedicationId",
                table: "DroneMedicationsEntity",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
