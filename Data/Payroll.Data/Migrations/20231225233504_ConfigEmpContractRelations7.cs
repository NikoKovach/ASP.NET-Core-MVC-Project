using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigEmpContractRelations7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
