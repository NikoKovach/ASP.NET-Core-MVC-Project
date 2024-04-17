using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigAnnexDepartmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annex_Departments_DepartmentID",
                table: "Annex");

            migrationBuilder.DropForeignKey(
                name: "FK_Annex_EmploymentContracts_EmpContractId",
                table: "Annex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Annex",
                table: "Annex");

            migrationBuilder.RenameTable(
                name: "Annex",
                newName: "Annexes");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Annexes",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Annex_EmpContractId",
                table: "Annexes",
                newName: "IX_Annexes_EmpContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Annex_DepartmentID",
                table: "Annexes",
                newName: "IX_Annexes_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Annexes",
                table: "Annexes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Annexes_Departments_DepartmentId",
                table: "Annexes",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Annexes_EmploymentContracts_EmpContractId",
                table: "Annexes",
                column: "EmpContractId",
                principalTable: "EmploymentContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annexes_Departments_DepartmentId",
                table: "Annexes");

            migrationBuilder.DropForeignKey(
                name: "FK_Annexes_EmploymentContracts_EmpContractId",
                table: "Annexes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Annexes",
                table: "Annexes");

            migrationBuilder.RenameTable(
                name: "Annexes",
                newName: "Annex");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Annex",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Annexes_EmpContractId",
                table: "Annex",
                newName: "IX_Annex_EmpContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Annexes_DepartmentId",
                table: "Annex",
                newName: "IX_Annex_DepartmentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Annex",
                table: "Annex",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Annex_Departments_DepartmentID",
                table: "Annex",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Annex_EmploymentContracts_EmpContractId",
                table: "Annex",
                column: "EmpContractId",
                principalTable: "EmploymentContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
