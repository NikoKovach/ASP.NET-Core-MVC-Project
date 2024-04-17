using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHasBeenDeletedColumnInSecondaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "TypeVacations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "TypeSickSheets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "PlaceOfRegistrationOrWork",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "ModeOfTreatments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "LaborCodeArticles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "EducationTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "DocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "ContractTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "TypeVacations");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "TypeSickSheets");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "ModeOfTreatments");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "LaborCodeArticles");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "EducationTypes");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "ContractTypes");
        }
    }
}
