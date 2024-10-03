using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class PersonFilterVM : SearchPersonVM
       {
              [StringLength( 100, MinimumLength = 3 )]
              [Display( Name = "Name" )]
              public string? SearchName { get; set; }
       }
}
