
using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class IdDocumentEmpVM
       {
              [Required]
              [Display( Name = "Document type" )]
              public string? DocumentName { get; set; }

              [Required]
              [Display( Name = "Document number" )]
              public string? DocumentNumber { get; set; }
       }
}
