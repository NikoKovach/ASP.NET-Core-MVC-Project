using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Payroll.ViewModels
{
	public class SearchPersonVM
	{
		public int Id { get; set; }
		
		public string  FirstName {get; set; }

		public string? MiddleName { get; set; }

		public string? LastName { get; set; }

		[Display(Name ="Unified Civil Number")]
		public string? EGN { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(this.FirstName + " ");

			if ( !string.IsNullOrEmpty(this.MiddleName) )
			{
				sb.Append(this.MiddleName + " ");
			}

			sb.Append(this.LastName + " ");
			sb.Append("-> Civil number : " + this.EGN);

			return sb.ToString();
		}
	}
}