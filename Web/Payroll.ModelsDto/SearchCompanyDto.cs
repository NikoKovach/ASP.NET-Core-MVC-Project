
using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto
{
	public class SearchCompanyDto
	{
		public int Id { get; set; }
		
		public string Name { get; set; }
		
		[Display(Name = "Unique Identifier")]
		public string UniqueIdentifier { get; set; }

		[Display(Name = "Companies List")]
		public string Info => $"{Name} -> UniqueId : {UniqueIdentifier}";


	}
}
