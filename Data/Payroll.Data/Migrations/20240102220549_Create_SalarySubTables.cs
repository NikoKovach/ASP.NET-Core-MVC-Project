using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_SalarySubTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeductionPartStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RateInPercent = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    SalaryStatementId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionPartStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeductionPartStatements_MonthlySalaryStatements_SalaryStatementId",
                        column: x => x.SalaryStatementId,
                        principalTable: "MonthlySalaryStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncomePartStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Days = table.Column<byte>(type: "tinyint", nullable: true),
                    Hours = table.Column<byte>(type: "tinyint", nullable: true),
                    RateInPercent = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    SalaryStatementId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomePartStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomePartStatements_MonthlySalaryStatements_SalaryStatementId",
                        column: x => x.SalaryStatementId,
                        principalTable: "MonthlySalaryStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecapPartStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20,2)", nullable: true),
                    SalaryStatementId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecapPartStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecapPartStatements_MonthlySalaryStatements_SalaryStatementId",
                        column: x => x.SalaryStatementId,
                        principalTable: "MonthlySalaryStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeductionPartStatements_SalaryStatementId",
                table: "DeductionPartStatements",
                column: "SalaryStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomePartStatements_SalaryStatementId",
                table: "IncomePartStatements",
                column: "SalaryStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_RecapPartStatements_SalaryStatementId",
                table: "RecapPartStatements",
                column: "SalaryStatementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeductionPartStatements");

            migrationBuilder.DropTable(
                name: "IncomePartStatements");

            migrationBuilder.DropTable(
                name: "RecapPartStatements");
        }
    }
}
