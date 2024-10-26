using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePayrollDB : Migration
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
                    AddressType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Entrance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyHeadquarter = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressOfManagement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UniqueIdentifier = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VATRegNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    RepresentedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RepresentativeIdNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CompanyCaseNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeductionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
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
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborCodeArticles", x => x.Id);
                });

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
                name: "PlaceOfRegistrationOrWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    Entance = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Floor = table.Column<int>(type: "int", nullable: true),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfRegistrationOrWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicHolidayAndWeekdays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicHoliday = table.Column<DateTime>(type: "date", nullable: true),
                    WorkDay = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicHolidayAndWeekdays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeSickSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSickSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeVacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeVacations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EGN = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PhotoFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    PermanentAddressId = table.Column<int>(type: "int", nullable: true),
                    CurrentAddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_CurrentAddressId",
                        column: x => x.CurrentAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_PermanentAddressId",
                        column: x => x.PermanentAddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfos_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    NumberFromTheList = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    EmpContractId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Employees_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeductionElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    DeductionTypeId = table.Column<int>(type: "int", nullable: true),
                    RateInPercent = table.Column<float>(type: "real", nullable: true),
                    AddedOnDate = table.Column<DateTime>(type: "date", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeductionElements_DeductionTypes_DeductionTypeId",
                        column: x => x.DeductionTypeId,
                        principalTable: "DeductionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeductionElements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContractDate = table.Column<DateTime>(type: "date", nullable: false),
                    ContractTypeId = table.Column<int>(type: "int", nullable: true),
                    WorkExperience = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SpecialtyWorkExperience = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LaborCodeArticleId = table.Column<int>(type: "int", nullable: false),
                    TrialPeriod = table.Column<int>(type: "int", nullable: true),
                    IsNegotiatedInFavorOf = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DeparmentId = table.Column<int>(type: "int", nullable: true),
                    EAC = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NCOP = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DayWorkTime = table.Column<byte>(type: "tinyint", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentSWE = table.Column<double>(type: "float", nullable: false),
                    SalaryDayOfTheMonth = table.Column<byte>(type: "tinyint", nullable: true),
                    PaidAnnualLeaveInDays = table.Column<byte>(type: "tinyint", nullable: false),
                    AdditionalPaidAnnualLeaveInDays = table.Column<byte>(type: "tinyint", nullable: false),
                    ProbationInMonths = table.Column<byte>(type: "tinyint", nullable: false),
                    NoticePeriodInDays = table.Column<byte>(type: "tinyint", nullable: false),
                    ReceivedAJobDescription = table.Column<bool>(type: "bit", nullable: false),
                    StartingWorkDate = table.Column<DateTime>(type: "date", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "date", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: true),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_Departments_DeparmentId",
                        column: x => x.DeparmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_LaborCodeArticles_LaborCodeArticleId",
                        column: x => x.LaborCodeArticleId,
                        principalTable: "LaborCodeArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "PlaceOfRegistrationOrWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_PlaceOfRegistrationOrWork_WorkPlaceId",
                        column: x => x.WorkPlaceId,
                        principalTable: "PlaceOfRegistrationOrWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
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

            migrationBuilder.CreateTable(
                name: "TemporaryDisabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SickSheetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "date", nullable: false),
                    TypeSickSheetId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AmbulatorySheetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RegNumberMedFacility = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IssuedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PublisherName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AddressOfThePublisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OnLeaveFrom = table.Column<DateTime>(type: "date", nullable: false),
                    OnLeaveUntil = table.Column<DateTime>(type: "date", nullable: false),
                    AllLeaveOnCalendarDays = table.Column<int>(type: "int", nullable: false),
                    CountOfWorkDays = table.Column<int>(type: "int", nullable: true),
                    DaysInAWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisAccordingToICD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateToNextExamination = table.Column<DateTime>(type: "date", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryDisabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryDisabilities_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemporaryDisabilities_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemporaryDisabilities_TypeSickSheets_TypeSickSheetId",
                        column: x => x.TypeSickSheetId,
                        principalTable: "TypeSickSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    TypeVacationId = table.Column<int>(type: "int", nullable: false),
                    OnLeaveFrom = table.Column<DateTime>(type: "date", nullable: false),
                    CountOfWorkDays = table.Column<int>(type: "int", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacations_TypeVacations_TypeVacationId",
                        column: x => x.TypeVacationId,
                        principalTable: "TypeVacations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeductionElementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_DeductionElements_DeductionElementId",
                        column: x => x.DeductionElementId,
                        principalTable: "DeductionElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeductionElementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationTypes_DeductionElements_DeductionElementId",
                        column: x => x.DeductionElementId,
                        principalTable: "DeductionElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IncomeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeductionElementId = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeTypes_DeductionElements_DeductionElementId",
                        column: x => x.DeductionElementId,
                        principalTable: "DeductionElements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Annexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgreementNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfAgreementOrChange = table.Column<DateTime>(type: "date", nullable: false),
                    CountedFromDate = table.Column<DateTime>(type: "date", nullable: false),
                    LaborCodeArticleId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EAC = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NCOP = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WorkPlaceId = table.Column<int>(type: "int", nullable: true),
                    DayWorkTime = table.Column<byte>(type: "tinyint", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PercentSWE = table.Column<double>(type: "float", nullable: true),
                    SalaryDayOfTheMonth = table.Column<byte>(type: "tinyint", nullable: true),
                    PaidAnnualLeaveInDays = table.Column<byte>(type: "tinyint", nullable: true),
                    AdditionalPaidAnnualLeaveInDays = table.Column<byte>(type: "tinyint", nullable: true),
                    NoticePeriodInDays = table.Column<byte>(type: "tinyint", nullable: true),
                    ReceivedAJobDescription = table.Column<bool>(type: "bit", nullable: true),
                    DateOfReceipt = table.Column<DateTime>(type: "date", nullable: true),
                    EmpContractId = table.Column<int>(type: "int", nullable: true),
                    ContractTypeId = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Annexes_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Annexes_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Annexes_EmploymentContracts_EmpContractId",
                        column: x => x.EmpContractId,
                        principalTable: "EmploymentContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "ModeOfTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TemporaryDisabilityId = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeOfTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeOfTreatments_TemporaryDisabilities_TemporaryDisabilityId",
                        column: x => x.TemporaryDisabilityId,
                        principalTable: "TemporaryDisabilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingDaysByMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    VacationId = table.Column<int>(type: "int", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "IdDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    DateOfExpire = table.Column<DateTime>(type: "date", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    ColorOfEyes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IssuingAuthority = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "date", nullable: false),
                    VehicleCategory = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    DateOfAcquisitionOfTheCategory = table.Column<DateTime>(type: "date", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_IdDocuments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiplomaRegNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "date", nullable: true),
                    Seria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    Speciality = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diplomas_EducationTypes_EducationId",
                        column: x => x.EducationId,
                        principalTable: "EducationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diplomas_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncomeElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    IncomeTypeId = table.Column<int>(type: "int", nullable: true),
                    Days = table.Column<byte>(type: "tinyint", nullable: true),
                    Hours = table.Column<byte>(type: "tinyint", nullable: true),
                    RateInPercent = table.Column<float>(type: "real", nullable: true),
                    AddedOnDate = table.Column<DateTime>(type: "date", nullable: true),
                    HasBeenDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeElements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncomeElements_IncomeTypes_IncomeTypeId",
                        column: x => x.IncomeTypeId,
                        principalTable: "IncomeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annexes_ContractTypeId",
                table: "Annexes",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Annexes_DepartmentId",
                table: "Annexes",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Annexes_EmpContractId",
                table: "Annexes",
                column: "EmpContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PersonId",
                table: "ContactInfos",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionElements_DeductionTypeId",
                table: "DeductionElements",
                column: "DeductionTypeId",
                unique: true,
                filter: "[DeductionTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionElements_EmployeeId",
                table: "DeductionElements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionPartStatements_SalaryStatementId",
                table: "DeductionPartStatements",
                column: "SalaryStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_EducationId",
                table: "Diplomas",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_PersonId",
                table: "Diplomas",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DeductionElementId",
                table: "DocumentTypes",
                column: "DeductionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationTypes_DeductionElementId",
                table: "EducationTypes",
                column: "DeductionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
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
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_LaborCodeArticleId",
                table: "EmploymentContracts",
                column: "LaborCodeArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_PlaceId",
                table: "EmploymentContracts",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_WorkPlaceId",
                table: "EmploymentContracts",
                column: "WorkPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_IdDocuments_DocumentTypeId",
                table: "IdDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdDocuments_PersonId",
                table: "IdDocuments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeElements_EmployeeId",
                table: "IncomeElements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeElements_IncomeTypeId",
                table: "IncomeElements",
                column: "IncomeTypeId",
                unique: true,
                filter: "[IncomeTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IncomePartStatements_SalaryStatementId",
                table: "IncomePartStatements",
                column: "SalaryStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTypes_DeductionElementId",
                table: "IncomeTypes",
                column: "DeductionElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ModeOfTreatments_TemporaryDisabilityId",
                table: "ModeOfTreatments",
                column: "TemporaryDisabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySalaryStatements_EmployeeId",
                table: "MonthlySalaryStatements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySalaryStatements_PaymentTypeId",
                table: "MonthlySalaryStatements",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CurrentAddressId",
                table: "Persons",
                column: "CurrentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PermanentAddressId",
                table: "Persons",
                column: "PermanentAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RecapPartStatements_SalaryStatementId",
                table: "RecapPartStatements",
                column: "SalaryStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDisabilities_EmployeeId",
                table: "TemporaryDisabilities",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDisabilities_GenderId",
                table: "TemporaryDisabilities",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryDisabilities_TypeSickSheetId",
                table: "TemporaryDisabilities",
                column: "TypeSickSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeId",
                table: "Vacations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_TypeVacationId",
                table: "Vacations",
                column: "TypeVacationId");

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
                name: "Annexes");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "DeductionPartStatements");

            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "IdDocuments");

            migrationBuilder.DropTable(
                name: "IncomeElements");

            migrationBuilder.DropTable(
                name: "IncomePartStatements");

            migrationBuilder.DropTable(
                name: "ModeOfTreatments");

            migrationBuilder.DropTable(
                name: "PublicHolidayAndWeekdays");

            migrationBuilder.DropTable(
                name: "RecapPartStatements");

            migrationBuilder.DropTable(
                name: "WorkingDaysByMonths");

            migrationBuilder.DropTable(
                name: "EmploymentContracts");

            migrationBuilder.DropTable(
                name: "EducationTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "IncomeTypes");

            migrationBuilder.DropTable(
                name: "MonthlySalaryStatements");

            migrationBuilder.DropTable(
                name: "TemporaryDisabilities");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "LaborCodeArticles");

            migrationBuilder.DropTable(
                name: "PlaceOfRegistrationOrWork");

            migrationBuilder.DropTable(
                name: "DeductionElements");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "TypeSickSheets");

            migrationBuilder.DropTable(
                name: "TypeVacations");

            migrationBuilder.DropTable(
                name: "DeductionTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
