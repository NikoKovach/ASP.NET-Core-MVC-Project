using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmpContractViewModels
{
       public class DepartmentVM : ValidateBaseModel
       {
              public int? DepartmentId { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Name { get; set; }
       }
}
