
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.ModelsDto;
using Payroll.Services.Services.ServiceContracts;

namespace Payroll.Services.Services.CompanyServices
{
	public class ComponentSearchCompany : IAllValidEntities<SearchCompanyDto>
	{
		private PayrollContext db;

		public ComponentSearchCompany(PayrollContext contex)
		{
			this.db = contex;
		}

		public async Task<IList<SearchCompanyDto>> GetAllValidEntitiesAsync()
		{
			var companyList = await this.db.Companies
				.Where(x => x.HasBeenDeleted == false)
				.Select(x => new SearchCompanyDto 
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
