using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
    public interface ILaborCodeArticleService : IBasicAddUpdate<LaborCodeArticleVM>,
                                                IBasicGetEntityIQueryable<string?>
    {
        IQueryable<LaborCodeArticleVM>? AllArticles( );
    }
}
