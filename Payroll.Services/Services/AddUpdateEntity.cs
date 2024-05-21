using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Services.Services.ServiceContracts;

using static Payroll.Services.Utilities.Messages.ExceptionMessages;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services
{
     public class AddUpdateEntity: IAddUpdateEntity
        
     {
          private PayrollContext context;
          private IMapper mapper;

          public AddUpdateEntity(PayrollContext payrollContext, IMapper autoMapper) : this(payrollContext)
          {
               ArgumentNullConfirmation( autoMapper,nameof(autoMapper ), GetClassName() ,GetClassFullName( ));

               this.mapper = autoMapper;
          }

          public AddUpdateEntity(PayrollContext payrollContext)
          {
               ArgumentNullConfirmation( payrollContext,nameof(payrollContext ),GetClassName() ,GetClassFullName( ));

               this.context = payrollContext;
          }

          public async Task AddEntityAsync<TEntity,TSource>( TSource entityDto )
          {
               ArgumentNullConfirmation<TSource>( entityDto, nameof( entityDto ), nameof( AddEntityAsync ), GetClassFullName() );

               TEntity entity = CreateObject<TEntity,TSource>(entityDto);

               string entityName = entity.GetType().Name;

               EntityNullConfirmation<TEntity>( entity,entityName,nameof(AddEntityAsync),GetClassFullName() );

               if ( await AddRecordAsync( entity ) == false )
               {
                    throw new InvalidOperationException( AddOrUpdateError );
               }
          }

          public async Task UpdateEntityAsync<TEntity,TSource>( TSource entityDto )
          {
               ArgumentNullConfirmation( entityDto, nameof( entityDto ), nameof( UpdateEntityAsync ), GetClassFullName() );

               TEntity entity = CreateObject<TEntity,TSource>(entityDto);

               EntityNullConfirmation<TEntity>( entity, nameof( TEntity ), nameof( UpdateEntityAsync ), GetClassFullName() );

               if ( await UpdateRecordAsync( entity ) == false )
               {
                    throw new InvalidOperationException( AddOrUpdateError );
               }
          }

          public virtual async Task<bool> AddRecordAsync<TEntity>(TEntity entity)
          {
               try
               {
                    await this.context.AddAsync(entity);

                    await this.context.SaveChangesAsync();

                    return true;
               }
               catch (Exception)
               {

                    return false;
               }
          }

          public virtual async Task<bool> UpdateRecordAsync<TEntity>( TEntity entity )
          {
               try
               {
                    var updatedEntity = context.Entry( entity );

                    if (updatedEntity.State != EntityState.Detached)
                    {
                         updatedEntity.State = EntityState.Detached;
                    }

                    if ( updatedEntity.State == EntityState.Detached )
                    {
                         //DbSet<TEntity> dbSet = this.context.Set<TEntity>();
                         context.Attach( entity );
                    }

                    updatedEntity.State = EntityState.Modified;

                    await this.context.SaveChangesAsync();

                    return true;
               }
               catch ( Exception )
               {
                    return false;
               }
          }

          public virtual TEntity CreateObject<TEntity,TSource>(TSource view)
          {
               TEntity entity = mapper.Map<TEntity>(view);

               return entity ;
          }

          private string GetClassName( )
          {
               return this.GetType().Name;
          }

          private string? GetClassFullName( )
          {
               return this.GetType().FullName;
          }
     }
}
