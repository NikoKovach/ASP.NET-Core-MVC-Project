using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipBetweenAddressAndEmpContractTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_PlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_WorkPlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropTable(
                name: "PlaceOfRegistrationOrWork");

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Addresses_PlaceId",
                table: "EmploymentContracts",
                column: "PlaceId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_Addresses_WorkPlaceId",
                table: "EmploymentContracts",
                column: "WorkPlaceId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Addresses_PlaceId",
                table: "EmploymentContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentContracts_Addresses_WorkPlaceId",
                table: "EmploymentContracts");

            migrationBuilder.CreateTable(
                name: "PlaceOfRegistrationOrWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Entance = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfRegistrationOrWork", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_PlaceId",
                table: "EmploymentContracts",
                column: "PlaceId",
                principalTable: "PlaceOfRegistrationOrWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_WorkPlaceId",
                table: "EmploymentContracts",
                column: "WorkPlaceId",
                principalTable: "PlaceOfRegistrationOrWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
