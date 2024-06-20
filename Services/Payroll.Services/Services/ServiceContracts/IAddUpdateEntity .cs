using AutoMapper;
using Payroll.Data;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface IAddUpdateEntity 
     {
          Task AddEntityAsync<TEntity,TSource>(TSource entityDto)
			where TEntity : class
			where TSource : class;

		Task UpdateEntityAsync<TEntity, TSource>( TSource entityDto )
			where TEntity : class
			where TSource : class;

		public PayrollContext Context { get; }

		public IMapper Mapper { get; }
	}
}
