using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCompanyTable_AddAddressesNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadquartersAddressId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagementAddressId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_HeadquartersAddressId",
                table: "Companies",
                column: "HeadquartersAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ManagementAddressId",
                table: "Companies",
                column: "ManagementAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_HeadquartersAddressId",
                table: "Companies",
                column: "HeadquartersAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_ManagementAddressId",
                table: "Companies",
                column: "ManagementAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_HeadquartersAddressId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_ManagementAddressId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_HeadquartersAddressId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ManagementAddressId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HeadquartersAddressId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ManagementAddressId",
                table: "Companies");
        }
    }
}
