using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;

namespace Payroll.Services.Services.CompanyServices
{
	public class SearchPersonService : IAllValidEntities<SearchPersonVM>
	{
		private PayrollContext db;

		public SearchPersonService(PayrollContext contex)
		{
			this.db = contex;
		}

		public async Task<IList<SearchPersonVM>> GetAllValidEntitiesAsync()
		{
			List<SearchPersonVM> personList = await this.db.Persons
				.Select(x => new SearchPersonVM 
				{ 
					Id		= x.Id,
					FirstName = x.FirstName,
					MiddleName = x.MiddleName,
					LastName = x.LastName,
					EGN = x.EGN
				} )
				.OrderBy(x => x.FirstName)
				.ThenBy(x => x.LastName)
				.ToListAsync();

			return personList;
		}
	}
}
