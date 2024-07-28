using AutoMapper;
using Payroll.Data;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface IAddUpdateEntity 
     {
		Task AddEntityAsync<TEntity>( TEntity entity )
			where TEntity : class;

		void UpdateEntity<TEntity>( TEntity entity )
			where TEntity : class;

		Task SaveAsync();

		PayrollContext Context { get;}
	}
}
