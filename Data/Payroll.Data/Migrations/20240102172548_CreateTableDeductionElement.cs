using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableDeductionElement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeductionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeductionElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    DeductionTypeId = table.Column<int>(type: "int", nullable: true),
                    RateInPercent = table.Column<float>(type: "real", nullable: true),
                    AddedOnDate = table.Column<DateTime>(type: "date", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeductionElements_DeductionTypes_DeductionTypeId",
                        column: x => x.DeductionTypeId,
                        principalTable: "DeductionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeductionElements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeductionElements_DeductionTypeId",
                table: "DeductionElements",
                column: "DeductionTypeId",
                unique: true,
                filter: "[DeductionTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionElements_EmployeeId",
                table: "DeductionElements",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeductionElements");

            migrationBuilder.DropTable(
                name: "DeductionTypes");
        }
    }
}
