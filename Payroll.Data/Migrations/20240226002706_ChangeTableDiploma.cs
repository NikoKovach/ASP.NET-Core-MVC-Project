using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableDiploma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiplomaNumber",
                table: "Diplomas",
                newName: "DiplomaRegNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIssue",
                table: "Diplomas",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Seria",
                table: "Diplomas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Diplomas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Diplomas");

            migrationBuilder.DropColumn(
                name: "Seria",
                table: "Diplomas");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Diplomas");

            migrationBuilder.RenameColumn(
                name: "DiplomaRegNumber",
                table: "Diplomas",
                newName: "DiplomaNumber");
        }
    }
}
