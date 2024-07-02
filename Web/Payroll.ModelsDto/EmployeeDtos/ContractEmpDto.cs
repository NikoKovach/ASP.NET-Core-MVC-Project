
using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto.EmployeeDtos
{
	public class ContractEmpDto
	{
		[Display(Name = "Job Title")]
		public string? JobTitle		{ get; set; }

		[Display(Name = "Department")]
          public string? DepartmentName { get; set; }

		[Display(Name = "Contract Type")]
		public string? ContractType	{ get; set; }

		[Display(Name = "Contract Number")]
		public string? ContractNumber { get; set; }

		[Display(Name = "Contract Date")]
          public DateTime? ContractDate { get; set; }
	}
}
