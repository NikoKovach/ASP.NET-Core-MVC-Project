using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateMonthlySalaryStatementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlySalaryStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<byte>(type: "tinyint", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    WorkDays = table.Column<byte>(type: "tinyint", nullable: true),
                    DaysWorked = table.Column<byte>(type: "tinyint", nullable: true),
                    GeneralInternship = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ProfessionalInternship = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PublicInternship = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SocialSecurityIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlySalaryStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlySalaryStatements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MonthlySalaryStatements_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySalaryStatements_EmployeeId",
                table: "MonthlySalaryStatements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySalaryStatements_PaymentTypeId",
                table: "MonthlySalaryStatements",
                column: "PaymentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlySalaryStatements");

            migrationBuilder.DropTable(
                name: "PaymentTypes");
        }
    }
}
