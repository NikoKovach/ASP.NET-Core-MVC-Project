
using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
	public class IdDocumentEmpVM
	{
		[Display(Name ="Document type")]
		public string? DocumentName { get; set; }

		[Display(Name ="Document number")]
          public string? DocumentNumber { get; set; } 
	}
}
