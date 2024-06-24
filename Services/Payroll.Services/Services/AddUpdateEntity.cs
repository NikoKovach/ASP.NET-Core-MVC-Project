using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Services.Services.ServiceContracts;

using static Payroll.Services.Utilities.Messages.ExceptionMessages;

namespace Payroll.Services.Services
{
	public class AddUpdateEntity : IAddUpdateEntity
	{
		private PayrollContext context;

		public AddUpdateEntity( PayrollContext payrollContext )
		{
			//ArgumentNullConfirmation( payrollContext, nameof( payrollContext ), GetClassName(), GetClassFullName() );

			this.context = payrollContext;
		}

		public PayrollContext Context => this.context;

		public virtual async Task AddEntityAsync<TEntity>( TEntity entity )
			where TEntity : class
		{
			//ArgumentNullConfirmation( entity, nameof( entity ), nameof( AddEntityAsync ), GetClassFullName() );

			try
			{
				await this.context.AddAsync( entity );
			}
			catch ( Exception)
			{
				throw new InvalidOperationException( AddOrUpdateError );
			}
		}

		public virtual void UpdateEntity<TEntity>( TEntity modifyEntity )
			where TEntity : class
		{
			//ArgumentNullConfirmation( modifyEntity, nameof( modifyEntity ), nameof( UpdateEntity ), GetClassFullName() );

			try
			{
				var updatedEntity = context.Entry( modifyEntity );

				if ( updatedEntity.State != EntityState.Detached )
				{
					updatedEntity.State = EntityState.Detached;
				}

				if ( updatedEntity.State == EntityState.Detached )
				{
					context.Attach( modifyEntity );
				}

				updatedEntity.State = EntityState.Modified;

			}
			catch ( Exception)
			{
				throw new InvalidOperationException( AddOrUpdateError );
			}
		}

		public async Task SaveAsync()
		{
			try
			{
				await this.context.SaveChangesAsync();
			}
			catch ( Exception )
			{
				throw new InvalidOperationException( AddOrUpdateError );
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