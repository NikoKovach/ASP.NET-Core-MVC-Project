
using LegalFramework.Services.DocumentGenerator;

using NUnit.Framework;

using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Test
{
    [TestFixture]
    public class TestTempDocument
    {
        private object[]? docModels;

        [SetUp]
        public void SetUp( )
        {
            LaborAgreementVM contractModel = GetContractVM( );

            this.docModels = [contractModel,
                                //conclusionAddressVM,
                                //            company,
                                //            companyHeadquartersVM,
                                //            employee
                            ];
        }

        [TearDown]
        public void TearDown( )
        {
            this.docModels = default;
        }


        [Test]
        public void Method_CreateFile_ThrowsException( )
        {
            var tempDoc = new TempDocument( );

            tempDoc.CreateFile( string.Empty, null, this.docModels );

        }

        //####################################################################

        private LaborAgreementVM GetContractVM( )
        {
            return new LaborAgreementVM
            {
                AdditionalPaidAnnualLeaveInDays = 5,
                CompanyId = 5,
                ContractDate = DateTime.Parse( "5.01.2025" ),
                ContractNumber = "5555555555",
                ContractTypeId = 1,
                DateOfReceipt = DateTime.Parse( "5.01.2025" ),
                DepartmentId = 3,
                EAC = "Something",
                EmployeeId = 13,
                FirstLastName = null,
                FirstName = null,
                HasBeenDeleted = null,
                Id = null,
                IsActive = true,
                IsNegotiatedInFavorOf = null,
                IsRegistered = true,
                JobTitle = "Accountant",
                LaborCodeArticle = null,
                LaborCodeArticleId = 1,
                LastName = null,
                NCOP = "7001-2325",
                NoticePeriodInDays = 30,
                PaidLeaveInDays = 25,
                PercentSWE = 0.06,
                PlaceId = 37,
                ProbationInMonths = 6,
                ReceivedAJobDescription = null,
                Salary = 4000,
                SalaryDayOfTheMonth = 20,
                SpecialtyWorkExperience = null,
                StartingWorkDate = DateTime.Parse( "5.01.2025" ),
                TrialPeriod = 30,
                WorkExperience = null,
                WorkPlaceId = 37,
                WorkTime = 8,
            };
        }
    }
}

//object[] docModels =
//            [
//                contractModel,
//    conclusionAddressVM,
//                company,
//                companyHeadquartersVM,
//                employee
//            ];

//this.document = new Document( );
//Section section = document.AddSection( );
//this.paragraph = section.AddParagraph( );

//this.testString = "TestValue";