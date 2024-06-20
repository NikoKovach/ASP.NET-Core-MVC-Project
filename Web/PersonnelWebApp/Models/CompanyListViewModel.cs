using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonnelWebApp.Models
{
	public class CompanyListViewModel
	{
		public int CompanyId { get; set; }

		public List<SelectListItem> Companies { set; get; } 
								= new List<SelectListItem>();
	}
}
