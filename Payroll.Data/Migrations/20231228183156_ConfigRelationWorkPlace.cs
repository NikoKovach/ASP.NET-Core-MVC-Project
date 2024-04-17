using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfigRelationWorkPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "EmploymentContracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WorkPlaceId",
                table: "EmploymentContracts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_WorkPlaceId",
                table: "EmploymentContracts",
                column: "WorkPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_WorkPlaceId",
                table: "EmploymentContracts",
                column: "WorkPlaceId",
                principalTable: "PlaceOfRegistrationOrWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_WorkPlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentContracts_WorkPlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropColumn(
                name: "WorkPlaceId",
                table: "EmploymentContracts");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
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
