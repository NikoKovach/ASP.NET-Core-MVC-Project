using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameToDayWorkTimeInTableEmploymentContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkTime",
                table: "EmploymentContracts",
                newName: "DayWorkTime");

            migrationBuilder.AlterColumn<byte>(
                name: "DayWorkTime",
                table: "EmploymentContracts",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "DayWorkTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayWorkTime",
                table: "EmploymentContracts",
                newName: "WorkTime");

            migrationBuilder.AlterColumn<byte>(
                name: "WorkTime",
                table: "EmploymentContracts",
                type: "DayWorkTime",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }
    }
}
