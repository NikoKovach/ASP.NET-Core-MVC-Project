using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmpContractViewModels
{
       public class LaborCodeArticleVM
       {
              public int? Id { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 4 )]
              public string? Article { get; set; }
       }
}
