using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeletionDatePropertyinModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Vacations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "TypeVacations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "TypeSickSheets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "TemporaryDisabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "PlaceOfRegistrationOrWork",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "ModeOfTreatments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "LaborCodeArticles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeductionElementId",
                table: "IncomeTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "HasBeenDeleted",
                table: "IncomeElements",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "IncomeElements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "IdDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "EmploymentContracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeductionElementId",
                table: "EducationTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeductionElementId",
                table: "DocumentTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Diplomas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "DeductionTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "DeductionElements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "ContractTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "ContactInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Annexes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTypes_DeductionElementId",
                table: "IncomeTypes",
                column: "DeductionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationTypes_DeductionElementId",
                table: "EducationTypes",
                column: "DeductionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DeductionElementId",
                table: "DocumentTypes",
                column: "DeductionElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_DeductionElements_DeductionElementId",
                table: "DocumentTypes",
                column: "DeductionElementId",
                principalTable: "DeductionElements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationTypes_DeductionElements_DeductionElementId",
                table: "EducationTypes",
                column: "DeductionElementId",
                principalTable: "DeductionElements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTypes_DeductionElements_DeductionElementId",
                table: "IncomeTypes",
                column: "DeductionElementId",
                principalTable: "DeductionElements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_DeductionElements_DeductionElementId",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationTypes_DeductionElements_DeductionElementId",
                table: "EducationTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTypes_DeductionElements_DeductionElementId",
                table: "IncomeTypes");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTypes_DeductionElementId",
                table: "IncomeTypes");

            migrationBuilder.DropIndex(
                name: "IX_EducationTypes_DeductionElementId",
                table: "EducationTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_DeductionElementId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "TypeVacations");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "TypeSickSheets");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "TemporaryDisabilities");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "ModeOfTreatments");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "LaborCodeArticles");

            migrationBuilder.DropColumn(
                name: "DeductionElementId",
                table: "IncomeTypes");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "IncomeElements");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "IdDocuments");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "EmploymentContracts");

            migrationBuilder.DropColumn(
                name: "DeductionElementId",
                table: "EducationTypes");

            migrationBuilder.DropColumn(
                name: "DeductionElementId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Diplomas");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "DeductionTypes");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "DeductionElements");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "ContractTypes");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Annexes");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Addresses");

            migrationBuilder.AlterColumn<bool>(
                name: "HasBeenDeleted",
                table: "IncomeElements",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
