using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class ContractEmpVM
       {
              [Display( Name = "Job title" )]
              public string? JobTitle { get; set; }

              [Display( Name = "Department" )]
              public string? DepartmentName { get; set; }

              [Display( Name = "Contract type" )]
              public string? ContractType { get; set; }

              [Display( Name = "Contract number" )]
              public string? ContractNumber { get; set; }

              [Display( Name = "Contract date" )]
              public string? ContractDate { get; set; }

              public int? AgreementsCount { get; set; }

              public AnnexJobTitleVM? LastAnnex { get; set; }

              [Display( Name = "Job Title" )]
              public string? CurrentJobTitle => ( this.LastAnnex != null ) ? this.LastAnnex.JobTitle : this.JobTitle;

              [Display( Name = "Department" )]
              public string? CurrentDepartmentName =>
                     ( this.LastAnnex != null ) ? this.LastAnnex.DepartmentName : this.DepartmentName;
       }
}

//public static ContractEmpVM? Create( IQueryable<> employee )
//{
//       ContractEmpVM? viewModel = new ContractEmpVM
//       {
//              JobTitle = employee.EmploymentContract.JobTitle,
//              DepartmentName = employee.EmploymentContract.Department.Name,
//              ContractType = employee.EmploymentContract.ContractType.Type,
//              ContractNumber = employee.EmploymentContract.ContractNumber,
//              ContractDate = employee.EmploymentContract.ContractDate.ToShortDateString(),
//              LastAnnex = employee.EmploymentContract
//                                   .SupplementaryAgreements
//                                   .Select( a => new AnnexJobTitleVM
//                                   {
//                                          Id = a.Id,
//                                          JobTitle = a.JobTitle,
//                                          DepartmentName = a.Department.Name
//                                   } )
//                                   .OrderBy( x => x.Id )
//                                  .LastOrDefault()


//       };

//       return viewModel;
//}