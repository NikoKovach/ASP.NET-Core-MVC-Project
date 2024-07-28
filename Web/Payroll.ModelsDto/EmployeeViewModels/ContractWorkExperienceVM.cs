using Payroll.Models;

namespace Payroll.ViewModels.EmployeeViewModels
{
	public class ContractWorkExperienceVM
	{
		public string? WorkExperience { get; set; } 

          public string? SpecialtyWorkExperience  { get; set; }

		public DateTime StartingWorkDate { get; set; }

		public List<Annex> Annexes { get; set; } = new List<Annex>();
	}
}
