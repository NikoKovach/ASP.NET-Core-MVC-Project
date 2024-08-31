using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class AnnexJobTitleVM
       {
              [Display( Name = "Annex Id" )]
              public int? Id { get; set; }

              [Display( Name = "Job Title" )]
              public string? JobTitle { get; set; }

              [Display( Name = "Department" )]
              public string? DepartmentName { get; set; }
       }
}
