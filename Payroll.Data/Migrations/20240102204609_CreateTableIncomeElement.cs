using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableIncomeElement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeTypes",
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
                    table.PrimaryKey("PK_IncomeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    IncomeTypeId = table.Column<int>(type: "int", nullable: true),
                    Days = table.Column<byte>(type: "tinyint", nullable: true),
                    Hours = table.Column<byte>(type: "tinyint", nullable: true),
                    RateInPercent = table.Column<float>(type: "real", nullable: true),
                    AddedOnDate = table.Column<DateTime>(type: "date", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeElements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncomeElements_IncomeTypes_IncomeTypeId",
                        column: x => x.IncomeTypeId,
                        principalTable: "IncomeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomeElements_EmployeeId",
                table: "IncomeElements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeElements_IncomeTypeId",
                table: "IncomeElements",
                column: "IncomeTypeId",
                unique: true,
                filter: "[IncomeTypeId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeElements");

            migrationBuilder.DropTable(
                name: "IncomeTypes");
        }
    }
}
