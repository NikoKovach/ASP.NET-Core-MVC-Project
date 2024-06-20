using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableCompanyAndEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employee_ManagerID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Company_CompanyId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Persons_PersonId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Employee_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Employee_EmployeeId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_PersonId",
                table: "Employees",
                newName: "IX_Employees_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_CompanyId",
                table: "Employees",
                newName: "IX_Employees_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_ManagerID",
                table: "Departments",
                column: "ManagerID",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Persons_PersonId",
                table: "Employees",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Employees_EmployeeId",
                table: "Persons",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_ManagerID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Persons_PersonId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Employees_EmployeeId",
                table: "EmploymentContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Employees_EmployeeId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PersonId",
                table: "Employee",
                newName: "IX_Employee_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyId",
                table: "Employee",
                newName: "IX_Employee_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employee_ManagerID",
                table: "Departments",
                column: "ManagerID",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Company_CompanyId",
                table: "Employee",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Persons_PersonId",
                table: "Employee",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Employee_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Employee_EmployeeId",
                table: "Persons",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
