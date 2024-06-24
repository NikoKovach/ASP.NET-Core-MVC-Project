namespace Payroll.Services.Services.ServiceContracts
{
	public interface IAddUpdate<TViewModel>
	{
		Task AddAsync( TViewModel viewModel );

		Task UpdateAsync( TViewModel viewModel );
	}
}
