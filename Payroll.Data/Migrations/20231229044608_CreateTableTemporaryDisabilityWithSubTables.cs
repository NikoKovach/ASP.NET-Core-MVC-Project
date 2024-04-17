using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableTemporaryDisabilityWithSubTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeSickSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSickSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryDisabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SickSheetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "date", nullable: false),
                    TypeSickSheetId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AmbulatorySheetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RegNumberMedFacility = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PublisherName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressOfThePublisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OnLeaveFrom = table.Column<DateTime>(type: "date", nullable: false),
                    OnLeaveUntil = table.Column<DateTime>(type: "date", nullable: false),
                    AllLeaveOnCalendarDays = table.Column<int>(type: "int", nullable: false),
                    DaysInAWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisAccordingToICD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateToNextExamination = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryDisabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryDisabilities_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemporaryDisabilities_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemporaryDisabilities_TypeSickSheets_TypeSickSheetId",
                        column: x => x.TypeSickSheetId,
                        principalTable: "TypeSickSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModeOfTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TemporaryDisabilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeOfTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeOfTreatments_TemporaryDisabilities_TemporaryDisabilityId",
                        column: x => x.TemporaryDisabilityId,
                        principalTable: "TemporaryDisabilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModeOfTreatments_TemporaryDisabilityId",
                table: "ModeOfTreatments",
                column: "TemporaryDisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDisabilities_EmployeeId",
                table: "TemporaryDisabilities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDisabilities_GenderId",
                table: "TemporaryDisabilities",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDisabilities_TypeSickSheetId",
                table: "TemporaryDisabilities",
                column: "TypeSickSheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModeOfTreatments");

            migrationBuilder.DropTable(
                name: "TemporaryDisabilities");

            migrationBuilder.DropTable(
                name: "TypeSickSheets");
        }
    }
}
