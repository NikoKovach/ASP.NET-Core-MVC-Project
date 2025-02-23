using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmpContractViewModels
{
    public class LaborCodeArticleVM : ValidateBaseModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength( 200, MinimumLength = 4 )]
        public string? Article { get; set; }
    }
}
