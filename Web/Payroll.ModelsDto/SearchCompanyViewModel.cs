using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels
{
	public class SearchCompanyViewModel
	{
		public int Id { get; set; }
		
		public string Name { get; set; }
		
		[Display(Name = "Unique Identifier")]
		public string UniqueIdentifier { get; set; }

		[Display(Name = "Companies List")]
		public string Info => $"{Name} -> UniqueId : {UniqueIdentifier}";


	}
}
