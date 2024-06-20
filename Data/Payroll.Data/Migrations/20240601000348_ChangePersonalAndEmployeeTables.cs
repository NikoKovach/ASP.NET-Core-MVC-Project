using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePersonalAndEmployeeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Employees_EmployeeId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_EmployeeId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "HasBeenDeleted",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "IsActual",
                table: "Employees",
                newName: "IsPresent");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Persons_PersonId",
                table: "Employees",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Persons_PersonId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PersonId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "IsPresent",
                table: "Employees",
                newName: "IsActual");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Persons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenDeleted",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EmployeeId",
                table: "Persons",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Employees_EmployeeId",
                table: "Persons",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
