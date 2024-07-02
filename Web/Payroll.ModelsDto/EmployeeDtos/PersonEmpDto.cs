using Payroll.Models;
using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto.EmployeeDtos
{
     public class PersonEmpDto
     {
		[Display(Name = "Person Id")]
          public int Id { get; set; }

		[Display(Name = "First Name")]
          public string  FirstName {get;set;}

		[Display(Name = "Middle Name")]
		public string? MiddleName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		public int? GenderId { get; set; }

		[Display(Name ="Gender")]
		public string? GenderType { get; set; }

		public string EGN { get; set; }

		[Display(Name = "Photo")]
		public string? PhotoFilePath { get; set; }

		//public int? PermanentAddressId { get; set; }

		//public int? CurrentAddressId { get; set; }

		//public Address? PermanentAddress { get; set; }

		//public Address? CurrentAddress { get; set; }

	}
}

//public  Address? PermanentAddress { get; set; }
//public  Address? CurrentAddress { get; set; }
//string Gender.Type { get; set; }
//public int? EmployeeId { get; set; }