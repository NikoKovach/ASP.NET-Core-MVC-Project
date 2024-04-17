using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePlaceOfRegistrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrations_PlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceOfRegistrations",
                table: "PlaceOfRegistrations");

            migrationBuilder.RenameTable(
                name: "PlaceOfRegistrations",
                newName: "PlaceOfRegistrationOrWork");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentNumber",
                table: "PlaceOfRegistrationOrWork",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Entance",
                table: "PlaceOfRegistrationOrWork",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "PlaceOfRegistrationOrWork",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "PlaceOfRegistrationOrWork",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "PlaceOfRegistrationOrWork",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceOfRegistrationOrWork",
                table: "PlaceOfRegistrationOrWork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_PlaceId",
                table: "EmploymentContracts",
                column: "PlaceId",
                principalTable: "PlaceOfRegistrationOrWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_PlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceOfRegistrationOrWork",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "Entance",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "PlaceOfRegistrationOrWork");

            migrationBuilder.RenameTable(
                name: "PlaceOfRegistrationOrWork",
                newName: "PlaceOfRegistrations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceOfRegistrations",
                table: "PlaceOfRegistrations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrations_PlaceId",
                table: "EmploymentContracts",
                column: "PlaceId",
                principalTable: "PlaceOfRegistrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
