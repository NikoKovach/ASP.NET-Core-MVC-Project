using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Payroll.ViewModels.CustomValidation;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
       public class ContactInfoVM : LaborContactVM
       {
              public int? Id { get; set; }

              [Display( Name = "Second E-mail" )]
              [StringLength( 20, MinimumLength = 3 )]
              [Order( 9 )]
              public string? E_MailAddress2 { get; set; }

              [Display( Name = "Deleted" )]
              [Order( 15 )]
              public bool HasBeenDeleted { get; set; }

              [Required]
              [Display( Name = "Person Id" )]
              [Order( 17 )]
              public int? PersonId { get; set; }
       }
}