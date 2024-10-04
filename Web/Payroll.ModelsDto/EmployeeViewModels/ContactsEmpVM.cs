using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class ContactsEmpVM
       {
              [Display( Name = "Phone number" )]
              [StringLength( 20, MinimumLength = 3 )]
              public string? PhoneNumberOne { get; set; }

              [Display( Name = "Phone number - 2" )]
              [StringLength( 20, MinimumLength = 3 )]
              public string? PhoneNumberTwo { get; set; }

              [Display( Name = "E-mail" )]
              [StringLength( 20, MinimumLength = 3 )]
              public string? E_MailAddress1 { get; set; }

              [StringLength( 20, MinimumLength = 3 )]
              [Display( Name = "Website" )]
              public string? WebSite { get; set; }
       }
}
