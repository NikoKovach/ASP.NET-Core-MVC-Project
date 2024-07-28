using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
     public class EmployeeVM
     {
		[Display(Name = "Employee Id")]
          public int? Id { get; set; }

		[Display(Name = "Person Id")]
		[Required(ErrorMessage = "The field is required.")]
          public int PersonId { get; set; }

		[Display(Name = "Company Id")]
		[Required(ErrorMessage = "Please select a company !")]
          public int CompanyId { get; set; }

		[Display(Name = "List Number")]
		public string? NumberFromTheList { get; set; }

		[Display(Name = "Employee Photo")]
		public IFormFile? EmpImage { get; set; }

		[Display(Name = "Is Present")]
		public bool IsPresent{ get; set; }
     }
}

          //public PersonDto? Person { get; set; }

          //public int? EmpContractId { get; set; }