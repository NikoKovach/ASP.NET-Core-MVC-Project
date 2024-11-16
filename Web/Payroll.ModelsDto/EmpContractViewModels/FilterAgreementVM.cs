using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmpContractViewModels
{
       public class FilterAgreementVM
       {
              [StringLength( 100, MinimumLength = 3 )]
              [Display( Name = "Employee Name" )]
              public string? SearchName { get; set; }

              public string? FirstName { get; set; }

              public string? LastName { get; set; }

              [StringLength( 30, MinimumLength = 2 )]
              [Display( Name = "Contract No" )]
              public string? ContractNumber { get; set; }

              [StringLength( 30, MinimumLength = 3 )]
              [Display( Name = "Job Title" )]
              public string? JobTitle { get; set; }

              [StringLength( 30, MinimumLength = 3 )]
              public string? Department { get; set; }

              [Display( Name = "From Date" )]
              public DateTime? StartContractDate { get; set; }

              [Display( Name = "To Date" )]
              public DateTime? EndContractDate { get; set; }
       }
}
