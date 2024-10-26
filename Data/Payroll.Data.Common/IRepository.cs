namespace Payroll.Data.Common
{
       using System;
       using System.Linq;
       using System.Threading.Tasks;
       using Microsoft.EntityFrameworkCore;

       public interface IRepository<TEntity> : IDisposable where TEntity : class
       {
              IQueryable<TEntity> All();

              IQueryable<TEntity> AllAsNoTracking();

              Task AddAsync( TEntity entity );

              void Update( TEntity entity );

              void Update( ICollection<TEntity> entities );

              Task SaveChangesAsync();

              DbSet<TEntity> DbSet { get; set; }

              PayrollContext Context { get; set; }
       }
}

//PayrollContext Context { get; set; }
//void Delete(TEntity entity);