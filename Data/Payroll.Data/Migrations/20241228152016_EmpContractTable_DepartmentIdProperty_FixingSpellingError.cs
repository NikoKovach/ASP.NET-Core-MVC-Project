using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmpContractTable_DepartmentIdProperty_FixingSpellingError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Departments_DeparmentId",
                table: "EmploymentContracts");

            migrationBuilder.RenameColumn(
                name: "DeparmentId",
                table: "EmploymentContracts",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EmploymentContracts_DeparmentId",
                table: "EmploymentContracts",
                newName: "IX_EmploymentContracts_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Departments_DepartmentId",
                table: "EmploymentContracts",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Departments_DepartmentId",
                table: "EmploymentContracts");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "EmploymentContracts",
                newName: "DeparmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EmploymentContracts_DepartmentId",
                table: "EmploymentContracts",
                newName: "IX_EmploymentContracts_DeparmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Departments_DeparmentId",
                table: "EmploymentContracts",
                column: "DeparmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
