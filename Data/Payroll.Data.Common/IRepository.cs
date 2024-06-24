namespace Payroll.Data.Common
{
	using Microsoft.EntityFrameworkCore;
	using System;
    using System.Linq;
    using System.Threading.Tasks;

	public interface IRepository<TEntity> : IDisposable
        where TEntity : class
	{
		IQueryable<TEntity> All();

		IQueryable<TEntity> AllAsNoTracking();

		Task AddAsync(TEntity entity);

		void Update(TEntity entity);

		//void Delete(TEntity entity);

		Task SaveChangesAsync();

		//PayrollContext Context { get; set; }

		DbSet<TEntity> DbSet { get; set; }
	}
}
