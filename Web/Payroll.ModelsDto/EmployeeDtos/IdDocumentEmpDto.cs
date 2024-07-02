
using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto.EmployeeDtos
{
	public class IdDocumentEmpDto
	{
		[Display(Name ="Document Type")]
		public string? DocumentName { get; set; }

		[Display(Name ="Document Number")]
          public string? DocumentNumber { get; set; } 
	}
}
