using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePayrollDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Entance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatOfManagement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressOfManagement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bullstat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepresentativeIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyCaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaborCodeArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborCodeArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceOfRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfRegistrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumberOne = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberTwo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    E_MailAddress1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    E_MailAddress2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActuale = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiplomaNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    Speciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diplomas_EducationTypes_EducationId",
                        column: x => x.EducationId,
                        principalTable: "EducationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActual = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmploymentContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractDate = table.Column<DateTime>(type: "date", nullable: false),
                    ContractTypeId = table.Column<int>(type: "int", nullable: false),
                    WorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialtyWorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaborCodeArticleId = table.Column<int>(type: "int", nullable: false),
                    TrialPeriod = table.Column<int>(type: "int", nullable: true),
                    IsNegotiatedInFavorOf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeparmentId = table.Column<int>(type: "int", nullable: true),
                    EAC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NCOP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkTime = table.Column<byte>(type: "tinyint", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentSWE = table.Column<double>(type: "float", nullable: false),
                    SalaryDayOfTheMonth = table.Column<byte>(type: "tinyint", nullable: true),
                    PaidLeaveInDays = table.Column<byte>(type: "tinyint", nullable: false),
                    ProbationInMonths = table.Column<byte>(type: "tinyint", nullable: false),
                    NoticePeriodInDays = table.Column<byte>(type: "tinyint", nullable: false),
                    StartingDorkDate = table.Column<DateTime>(type: "date", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "date", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_Departments_DeparmentId",
                        column: x => x.DeparmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_LaborCodeArticles_LaborCodeArticleId",
                        column: x => x.LaborCodeArticleId,
                        principalTable: "LaborCodeArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_PlaceOfRegistrations_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "PlaceOfRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    PermanentAddressId = table.Column<int>(type: "int", nullable: true),
                    CurrentAddressId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_CurrentAddressId",
                        column: x => x.CurrentAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_PermanentAddressId",
                        column: x => x.PermanentAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<long>(type: "bigint", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOgBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOgExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    ColorOfEyes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfAcquisitionOfTheCategory = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdDocuments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PersonId",
                table: "ContactInfos",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerID",
                table: "Departments",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_EducationId",
                table: "Diplomas",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_PersonId",
                table: "Diplomas",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId",
                table: "Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PersonId",
                table: "Employee",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_ContractTypeId",
                table: "EmploymentContracts",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_DeparmentId",
                table: "EmploymentContracts",
                column: "DeparmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_EmployeeId",
                table: "EmploymentContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_LaborCodeArticleId",
                table: "EmploymentContracts",
                column: "LaborCodeArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_PlaceId",
                table: "EmploymentContracts",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdDocuments_DocumentTypeId",
                table: "IdDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdDocuments_PersonId",
                table: "IdDocuments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CurrentAddressId",
                table: "Persons",
                column: "CurrentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EmployeeId",
                table: "Persons",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PermanentAddressId",
                table: "Persons",
                column: "PermanentAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfos_Persons_PersonId",
                table: "ContactInfos",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employee_ManagerID",
                table: "Departments",
                column: "ManagerID",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_Persons_PersonId",
                table: "Diplomas",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Persons_PersonId",
                table: "Employee",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Persons_PersonId",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "EmploymentContracts");

            migrationBuilder.DropTable(
                name: "IdDocuments");

            migrationBuilder.DropTable(
                name: "EducationTypes");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "LaborCodeArticles");

            migrationBuilder.DropTable(
                name: "PlaceOfRegistrations");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
