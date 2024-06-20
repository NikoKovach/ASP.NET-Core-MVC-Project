using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableIdDocumentChangeColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOgExpire",
                table: "IdDocuments",
                newName: "DateOfExpire");

            migrationBuilder.RenameColumn(
                name: "DateOgBirth",
                table: "IdDocuments",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfExpire",
                table: "IdDocuments",
                newName: "DateOgExpire");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "IdDocuments",
                newName: "DateOgBirth");
        }
    }
}
