using System.ComponentModel.DataAnnotations;
using Payroll.ViewModels.CustomValidation;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class LaborContactVM : ValidateBaseModel
       {
              [Display( Name = "Phone number" )]
              [StringLength( 20, MinimumLength = 3 )]
              [Order( 3 )]
              public string? PhoneNumberOne { get; set; }

              [Display( Name = "Phone number - 2" )]
              [StringLength( 20, MinimumLength = 3 )]
              [Order( 5 )]
              public string? PhoneNumberTwo { get; set; }

              [Display( Name = "E-mail" )]
              [StringLength( 20, MinimumLength = 3 )]
              [Order( 7 )]
              public string? E_MailAddress1 { get; set; }

              [StringLength( 20, MinimumLength = 3 )]
              [Display( Name = "Website" )]
              [Order( 11 )]
              public string? WebSite { get; set; }
       }
}
