using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace LegalFramework.Services.CalculationOfWorkExperience
{
    public class CalculateExperience : ICalculateExperience
    {
        private PayrollContext db;

        public CalculateExperience( PayrollContext context )
        {
            db = context;
        }

        public async Task<WorkExperienceVM> CalculateAsync( int? companyId, int empId )
        {
            IQueryable<Employee> empDbSet = db
                                     .Set<Employee>( )
                                     .Where( x => x.CompanyId == companyId
                                           && x.Id == empId );

            ContractWorkExperienceVM? contract = await empDbSet
                .Select( x => new ContractWorkExperienceVM
                {
                    WorkExperience = x.EmploymentContract.WorkExperience,
                    SpecialtyWorkExperience = x.EmploymentContract
                                            .SpecialtyWorkExperience,
                    StartingWorkDate = x.EmploymentContract.StartingWorkDate,
                    Annexes = x.EmploymentContract
                                            .SupplementaryAgreements
                                            .ToList( )
                } )
                .FirstOrDefaultAsync( );

            WorkExperienceVM? experience = new WorkExperienceVM( );

            /*TODO :
				-когато отчетем отпуските - платенирнеплатени,болнични
				-имаме таблицата с почивните и работните дни през годинята

			//CalculateWorkExperience(contract,experience);

			//CalculateSpecialtyExperience(contract,experience);
			*/


            return experience;
        }

        private void CalculateWorkExperience( ContractWorkExperienceVM? contract,
            WorkExperienceVM experience )
        {
            if (string.IsNullOrEmpty( contract.WorkExperience )
                && contract.Annexes.Count == 0)
            {
                string experienceSpan = CalculateTimeSpan( contract.StartingWorkDate );
            };
        }

        private string CalculateTimeSpan( DateTime startingWorkDate )
        {
            var span = DateTime.UtcNow - startingWorkDate;


            return span.Days.ToString( );
        }

        //****************************************************************
        private void CalculateSpecialtyExperience( ContractWorkExperienceVM? contract,
            WorkExperienceVM experience )
        {
            throw new NotImplementedException( );
        }
    }
}
