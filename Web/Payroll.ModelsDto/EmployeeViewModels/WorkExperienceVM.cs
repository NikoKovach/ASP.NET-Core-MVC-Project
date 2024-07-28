using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
	public class WorkExperienceVM
	{
		[Display(Name = "Work experience")]
		public string? WorkExperience { get; set; } 

		[Display(Name = "Specialty experience")]
          public string? SpecialtyWorkExperience  { get; set; } 
	}
}
