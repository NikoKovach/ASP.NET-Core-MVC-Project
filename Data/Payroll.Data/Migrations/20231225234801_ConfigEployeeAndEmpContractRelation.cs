using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigEployeeAndEmpContractRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentContracts_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmploymentContracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpContractId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentContracts_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.DropColumn(
                name: "EmpContractId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmploymentContracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
