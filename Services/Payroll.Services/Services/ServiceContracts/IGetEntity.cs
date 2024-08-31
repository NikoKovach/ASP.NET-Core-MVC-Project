namespace Payroll.Services.Services.ServiceContracts
{
	public interface IGetEntity<TEntityView>
	{
		Task<TEntityView?> GetEntityAsync( int? entityId );
	}
}
