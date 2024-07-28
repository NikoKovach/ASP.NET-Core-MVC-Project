using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonnelWebApp.Models
{
	public class PersonListViewModel
	{
		public int PersonId { get; set; }

		public List<SelectListItem> Persons { set; get; } 
								= new List<SelectListItem>();
	}
}
