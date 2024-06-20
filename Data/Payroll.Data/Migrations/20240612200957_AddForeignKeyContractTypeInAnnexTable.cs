using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyContractTypeInAnnexTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContractTypeId",
                table: "EmploymentContracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContractTypeId",
                table: "Annexes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Annexes_ContractTypeId",
                table: "Annexes",
                column: "ContractTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annexes_ContractTypes_ContractTypeId",
                table: "Annexes",
                column: "ContractTypeId",
                principalTable: "ContractTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annexes_ContractTypes_ContractTypeId",
                table: "Annexes");

            migrationBuilder.DropIndex(
                name: "IX_Annexes_ContractTypeId",
                table: "Annexes");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "Annexes");

            migrationBuilder.AlterColumn<int>(
                name: "ContractTypeId",
                table: "EmploymentContracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
