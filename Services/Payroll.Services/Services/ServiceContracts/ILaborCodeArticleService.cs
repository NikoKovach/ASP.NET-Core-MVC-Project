using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface ILaborCodeArticleService : IBasicAddUpdate<LaborCodeArticleVM>
       {
              IQueryable<LaborCodeArticleVM>? AllArticles();

              IQueryable<LaborCodeArticleVM>? GetEntity( int? entityId );
       }
}
