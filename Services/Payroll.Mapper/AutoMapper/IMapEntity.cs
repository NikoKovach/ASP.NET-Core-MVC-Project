using Microsoft.EntityFrameworkCore;

namespace Payroll.Mapper.AutoMapper
{
     public interface IMapEntity
	{
		TResult Map<TSource,TResult>( TSource view )
			where TSource : class
			where TResult : class;

		IQueryable<TResult> ProjectTo<TSource, TResult>( DbSet<TSource> dbSet)
			where TSource : class
			where TResult : class;
	}
}
	
