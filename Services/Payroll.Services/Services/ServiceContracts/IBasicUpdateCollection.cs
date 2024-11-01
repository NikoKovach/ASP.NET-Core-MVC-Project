namespace Payroll.Services.Services.ServiceContracts
{
       public interface IBasicUpdateCollection<TViewModel>
       {
              Task UpdateAsync( ICollection<TViewModel> viewModel );
       }
}
