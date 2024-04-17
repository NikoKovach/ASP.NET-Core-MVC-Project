using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableAnnex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreementNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfAgreementOrChange = table.Column<DateTime>(type: "date", nullable: false),
                    CountedFromDate = table.Column<DateTime>(type: "date", nullable: false),
                    LaborCodeArticleId = table.Column<int>(type: "int", nullable: true),
                    DeparmentId = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EAC = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NCOP = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: true),
                    DayWorkTime = table.Column<byte>(type: "tinyint", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PercentSWE = table.Column<double>(type: "float", nullable: true),
                    SalaryDayOfTheMonth = table.Column<byte>(type: "tinyint", nullable: true),
                    PaidAnnualLeaveInDays = table.Column<byte>(type: "tinyint", nullable: true),
                    AdditionalPaidAnnualLeaveInDays = table.Column<byte>(type: "tinyint", nullable: true),
                    NoticePeriodInDays = table.Column<byte>(type: "tinyint", nullable: true),
                    ReceivedAJobDescription = table.Column<bool>(type: "bit", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "date", nullable: true),
                    EmpContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Annex_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_Annex_EmploymentContracts_EmpContractId",
                        column: x => x.EmpContractId,
                        principalTable: "EmploymentContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annex_DepartmentID",
                table: "Annex",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Annex_EmpContractId",
                table: "Annex",
                column: "EmpContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Annex");
        }
    }
}
