using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmpContractViewModels
{
       public class AgreementTypeVM
       {
              public int? Id { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 4 )]
              public string? Type { get; set; }
       }
}
