using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;

namespace Payroll.Services.Services.CompanyServices
{
	public class SearchCompanyService : IAllValidEntities<SearchCompanyViewModel>
	{
		private PayrollContext db;

		public SearchCompanyService(PayrollContext contex)
		{
			this.db = contex;
		}

		public async Task<IList<SearchCompanyViewModel>> GetAllValidEntitiesAsync()
		{
			var companyList = await this.db.Companies
				.Where(x => x.HasBeenDeleted == false)
				.Select(x => new SearchCompanyViewModel 
				{ 
					Id				= x.Id,
					Name				= x.Name,
					UniqueIdentifier	= x.UniqueIdentifier
				} )
				.OrderBy(x => x.Name)
				.ToListAsync();

			return companyList;
		}
	}
}
