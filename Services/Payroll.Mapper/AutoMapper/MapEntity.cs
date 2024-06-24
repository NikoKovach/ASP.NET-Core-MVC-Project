using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;

namespace Payroll.Mapper.AutoMapper
{
	public class MapEntity : IMapEntity
	{
		private IMapper mapper;

		private PayrollContext db;

		public MapEntity(PayrollContext context,IMapper autoMapper)
		{
			this.db = context;

			this.mapper = autoMapper;
		}

		public TResult Map<TSource,TResult>( TSource view )
			where TSource : class
			where TResult : class			
		{
			try
			{
				TResult entity = mapper.Map<TResult>( view );

				return entity;
			}
			catch ( Exception )
			{
				throw new InvalidOperationException( );
			}
		}

		public IQueryable<TResult> ProjectTo<TSource,TResult>
		(DbSet<TSource> dbSet) 
			where TSource : class
			where TResult : class
		{
			try
			{
				IQueryable<TResult> query = dbSet.ProjectTo<TResult>
									(mapper.ConfigurationProvider);

				return query;
			}
			catch ( Exception )
			{
				throw new InvalidOperationException( );
			}
		}
	}
}
