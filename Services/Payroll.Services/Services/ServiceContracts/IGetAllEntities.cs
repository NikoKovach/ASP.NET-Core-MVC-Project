namespace Payroll.Services.Services.ServiceContracts
{
	public interface IGetAllEntities
	{
		Task<ICollection<TEntityView>> GetAllEntitiesAsync<TEntityView>()
			where TEntityView:class;
	}
}
