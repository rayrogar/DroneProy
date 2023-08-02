using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Dispatch_Controller_RESTapi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DroneModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Serial = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Weigth = table.Column<int>(type: "INTEGER", nullable: false),
                    BateryState = table.Column<int>(type: "INTEGER", nullable: false),
                    StateId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drones_DroneModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "DroneModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Drones_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Weigth = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    DroneEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Medications_Drones_DroneEntityId",
                        column: x => x.DroneEntityId,
                        principalTable: "Drones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DroneMedicationsEntity",
                columns: table => new
                {
                    DroneId = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneMedicationsEntity", x => new { x.DroneId, x.MedicationId });
                    table.ForeignKey(
                        name: "FK_DroneMedicationsEntity_Drones_DroneId",
                        column: x => x.DroneId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroneMedicationsEntity_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DroneModel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lightweight" },
                    { 2, "Middleweight" },
                    { 3, "Cruiserweight" },
                    { 4, "Heavyweight" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "IDLE" },
                    { 2, "LOADING" },
                    { 3, "LOADED" },
                    { 4, "DELIVERING" },
                    { 5, "DELIVERED" },
                    { 6, "RETURNING" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroneMedicationsEntity_MedicationId",
                table: "DroneMedicationsEntity",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Drones_ModelId",
                table: "Drones",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Drones_StateId",
                table: "Drones",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DroneEntityId",
                table: "Medications",
                column: "DroneEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DroneMedicationsEntity");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "DroneModel");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
