using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
	public class ContactsEmpVM
	{
		[Display(Name ="Phone number")]
          public string? PhoneNumberOne { get; set; }

		[Display(Name ="Phone number - 2")]
          public string? PhoneNumberTwo { get; set; }

		[Display(Name ="E-mail")]
          public string? E_MailAddress1 { get; set; }

          public string? Website { get; set; }
	}
}
