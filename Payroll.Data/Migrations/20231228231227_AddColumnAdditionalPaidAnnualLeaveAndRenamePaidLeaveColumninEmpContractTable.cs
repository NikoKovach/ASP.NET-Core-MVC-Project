using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAdditionalPaidAnnualLeaveAndRenamePaidLeaveColumninEmpContractTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaidLeaveInDays",
                table: "EmploymentContracts",
                newName: "PaidAnnualLeaveInDays");

            migrationBuilder.AddColumn<byte>(
                name: "AdditionalPaidAnnualLeaveInDays",
                table: "EmploymentContracts",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalPaidAnnualLeaveInDays",
                table: "EmploymentContracts");

            migrationBuilder.RenameColumn(
                name: "PaidAnnualLeaveInDays",
                table: "EmploymentContracts",
                newName: "PaidLeaveInDays");
        }
    }
}
