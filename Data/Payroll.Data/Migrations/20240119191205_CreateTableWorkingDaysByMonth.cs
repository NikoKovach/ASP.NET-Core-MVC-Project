using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableWorkingDaysByMonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingDaysByMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    VacationId = table.Column<int>(type: "int", nullable: true),
                    TempDisabilityId = table.Column<int>(type: "int", nullable: true),
                    TemporaryDisabilityId = table.Column<int>(type: "int", nullable: true),
                    Month = table.Column<byte>(type: "tinyint", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    WorkDays = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDaysByMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDaysByMonths_TemporaryDisabilities_TemporaryDisabilityId",
                        column: x => x.TemporaryDisabilityId,
                        principalTable: "TemporaryDisabilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingDaysByMonths_Vacations_VacationId",
                        column: x => x.VacationId,
                        principalTable: "Vacations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDaysByMonths_TemporaryDisabilityId",
                table: "WorkingDaysByMonths",
                column: "TemporaryDisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDaysByMonths_VacationId",
                table: "WorkingDaysByMonths",
                column: "VacationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingDaysByMonths");
        }
    }
}
