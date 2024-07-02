using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto.EmployeeDtos
{
	public class ContactsEmpDto
	{
		[Display(Name ="Phone Number")]
          public string? PhoneNumberOne { get; set; }

		[Display(Name ="Phone Number - 2")]
          public string? PhoneNumberTwo { get; set; }

		[Display(Name ="E-mail")]
          public string? E_MailAddress1 { get; set; }

          public string? Website { get; set; }
	}
}
