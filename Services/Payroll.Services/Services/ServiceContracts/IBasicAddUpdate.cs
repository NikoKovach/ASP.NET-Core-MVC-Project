namespace Payroll.Services.Services.ServiceContracts
{
       public interface IBasicAddUpdate<TViewModel>
       {
              Task AddAsync( TViewModel viewModel );

              Task UpdateAsync( TViewModel viewModel );
       }
}
