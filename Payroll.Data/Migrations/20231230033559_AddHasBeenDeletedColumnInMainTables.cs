using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHasBeenDeletedColumnInMainTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "TemporaryDisabilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "IdDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "EmploymentContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Diplomas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "ContactInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Annexes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "TemporaryDisabilities");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "IdDocuments");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "EmploymentContracts");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Diplomas");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Annexes");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Addresses");
        }
    }
}
