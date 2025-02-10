using Microsoft.EntityFrameworkCore;

namespace Payroll.Data.Common
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		public Repository(PayrollContext payrollContext)
		{
			EntityConfirmation.ArgumentNullConfirmation(payrollContext, nameof(payrollContext),
				   GetClassName(), GetClassFullName());

			this.Context = payrollContext;

			this.DbSet = this.Context.Set<TEntity>();
		}

		public PayrollContext Context { get; set; }

		public DbSet<TEntity> DbSet { get; set; }

		public IQueryable<TEntity> All()
		{
			return this.DbSet;
		}

		public IQueryable<TEntity> AllAsNoTracking()
		{
			return this.DbSet.AsNoTracking();
		}

		public virtual async Task AddAsync(TEntity entity)
		{
			EntityConfirmation.ArgumentNullConfirmation(entity, nameof(entity),
				   nameof(AddAsync), GetClassFullName());

			try
			{
				await this.Context.AddAsync(entity);
			}
			catch (Exception)
			{
				throw new InvalidOperationException
							  (ExceptionMessages.AddOrUpdateError);
			}
		}

		public virtual void Update(TEntity modifyEntity)
		{
			EntityConfirmation.ArgumentNullConfirmation(modifyEntity,
				   nameof(modifyEntity), nameof(Update), GetClassFullName());

			ChangeEntityStateToModified(modifyEntity);
		}

		public void Update(ICollection<TEntity> modifiedEntities)
		{
			EntityConfirmation.ArgumentNullConfirmation(modifiedEntities,
				   nameof(modifiedEntities), nameof(Update), GetClassFullName());

			foreach (var item in modifiedEntities)
			{
				ChangeEntityStateToModified(item);
			}
		}

		public async Task SaveChangesAsync()
		{
			try
			{
				await this.Context.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw new InvalidOperationException
									 (ExceptionMessages.AddOrUpdateError);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Context?.Dispose();
			}
		}
		//*******************************************************

		private void ChangeEntityStateToModified(TEntity modifyEntity)
		{
			try
			{
				var updatedEntity = this.Context.Entry(modifyEntity);

				if (updatedEntity.State != EntityState.Detached)
				{
					updatedEntity.State = EntityState.Detached;
				}

				if (updatedEntity.State == EntityState.Detached)
				{
					this.Context.Attach(modifyEntity);
				}

				updatedEntity.State = EntityState.Modified;
			}
			catch (Exception)
			{
				throw new InvalidOperationException
									 (ExceptionMessages.AddOrUpdateError);
			}
		}

		private string GetClassName()
		{
			return this.GetType().Name;
		}

		private string? GetClassFullName()
		{
			return this.GetType().FullName;
		}
	}
}