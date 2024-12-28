using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToManyRelationshipBetweenAnnexesAndLCArticlesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Annexes_LaborCodeArticleId",
                table: "Annexes",
                column: "LaborCodeArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annexes_LaborCodeArticles_LaborCodeArticleId",
                table: "Annexes",
                column: "LaborCodeArticleId",
                principalTable: "LaborCodeArticles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annexes_LaborCodeArticles_LaborCodeArticleId",
                table: "Annexes");

            migrationBuilder.DropIndex(
                name: "IX_Annexes_LaborCodeArticleId",
                table: "Annexes");
        }
    }
}
