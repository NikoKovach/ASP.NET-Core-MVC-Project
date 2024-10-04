using System.ComponentModel.DataAnnotations;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
       public class ContactInfoVM : ContactsEmpVM
       {
              public int Id { get; set; }

              [Display( Name = "Person Id" )]
              public int PersonId { get; set; }

              [Display( Name = "Second E-mail" )]
              [StringLength( 20, MinimumLength = 3 )]
              public string? E_MailAddress2 { get; set; }

              [Display( Name = "Deleted" )]
              public bool HasBeenDeleted { get; set; }
       }
}