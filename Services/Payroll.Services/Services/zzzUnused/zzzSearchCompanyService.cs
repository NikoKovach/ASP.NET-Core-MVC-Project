//using Payroll.ViewModels;

//namespace Payroll.Services.Services.CompanyServices
//{
//	public class SearchCompanyService : IAllValidEntities<SearchCompanyVM>
//	{
//		private PayrollContext db;

//		public SearchCompanyService(PayrollContext contex)
//		{
//			this.db = contex;
//		}

//		public async Task<IList<SearchCompanyVM>> GetAllValidEntitiesAsync()
//		{
//			var companyList = await this.db.Companies
//				.Where(x => x.HasBeenDeleted == false)
//				.Select(x => new SearchCompanyVM 
//				{ 
//					Id				= x.Id,
//					Name				= x.Name,
//					UniqueIdentifier	= x.UniqueIdentifier
//				} )
//				.OrderBy(x => x.Name)
//				.ToListAsync();

//			return companyList;
//		}
//	}
//}
