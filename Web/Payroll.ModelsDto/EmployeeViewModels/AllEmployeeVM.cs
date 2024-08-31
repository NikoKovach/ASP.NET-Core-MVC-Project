
using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class AllEmployeeVM : BaseEmployeeVM
       {
              [Display( Name = "List number" )]
              public string? NumberFromTheList { get; set; }

              [Display( Name = "Name" )]
              public string? FullName { get; set; }

              [Display( Name = "Id Number" )]
              public string? EGN { get; set; }

              public ContractEmpVM? ContractInfo { get; set; }
       }
}

/*
//public string? JobTitle { get; set; }

              //public string? DepartmentName { get; set; }

              //[Display( Name = "Contract number" )]
              //public string? ContractNumber { get; set; }

              //[DataType( DataType.Date )]
              //[Display( Name = "Contract date" )]
              //public string? ContractDate { get; set; }

              //public AnnexJobTitleVM? LastAnnex { get; set; }

              //[Display( Name = "Job Title" )]
              //public string? RealJobTitle => ( this.LastAnnex != null ) ? this.LastAnnex.JobTitle : this.JobTitle;

              //[Display( Name = "Department" )]
              //public string? RealDepartmentName =>
              //       ( this.LastAnnex != null ) ? this.LastAnnex.DepartmentName : this.DepartmentName;
*/
