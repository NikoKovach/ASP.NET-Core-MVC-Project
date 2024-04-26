using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Services.Services.ServiceContracts;

using static Payroll.Services.Utilities.Messages.ExceptionMessages;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services
{
     public class CreateUpdateEntityService<TSource, TEntity> : ICreateUpdateEntity<TSource, TEntity>
           where TEntity : class
           where TSource : class
     {
          private PayrollContext context;
          private IMapper mapper;

          public CreateUpdateEntityService(PayrollContext payrollContext, IMapper autoMapper) : this(payrollContext)
          {
               ArgumentNullConfirmation( autoMapper,nameof(autoMapper ), GetClassName() ,GetClassFullName( ));

               this.mapper = autoMapper;
          }

          public CreateUpdateEntityService(PayrollContext payrollContext)
          {
               ArgumentNullConfirmation( payrollContext,nameof(payrollContext ),GetClassName() ,GetClassFullName( ));

               this.context = payrollContext;
          }

          public PayrollContext Context { get => this.context; }

          public IMapper Mapper { get => this.mapper ;  }

          public TEntity CreateEntity(TSource entityDto)
          {
               ArgumentNullConfirmation(entityDto,nameof(entityDto), nameof(CreateEntity),GetClassFullName());

               TEntity entity = CreateObject(entityDto);

               string entityName = entity.GetType().Name;

               EntityNullConfirmation<TEntity>( entity,entityName,nameof(CreateEntity),GetClassFullName() );

               if ( AddRecord( entity ) == false )
               {
                    throw new InvalidOperationException( AddOrUpdateError );
               }

               return entity;
          }

          public TEntity UpdateEntity(TSource entityDto)
          {
               ArgumentNullConfirmation(entityDto,nameof(entityDto), nameof(UpdateEntity), GetClassFullName());

               TEntity entity = CreateObject(entityDto);

               EntityNullConfirmation<TEntity>( entity,nameof(TEntity),nameof(UpdateEntity),GetClassFullName() );

               if (UpdateRecord(entity) == false)
               {
                    throw new InvalidOperationException(AddOrUpdateError);
               }

               return entity;
          }

          public virtual bool AddRecord(TEntity entity)
          {
               try
               {
                    this.context.Add(entity);

                    this.context.SaveChanges();

                    return true;
               }
               catch (Exception)
               {

                    return false;
               }
          }

          public virtual bool UpdateRecord(TEntity entity)
          {
               try
               {
                    var updatedEntity = context.Entry( entity );

                    if (updatedEntity.State == EntityState.Detached)
                    {
                        DbSet<TEntity> dbSet = this.context.Set<TEntity>();
                        dbSet.Attach(entity);
                    }

                    updatedEntity.State = EntityState.Modified;

                    this.context.SaveChanges();

                    return true;
               }
               catch (Exception)
               {
                    return false;
               }
          }

          public virtual TEntity CreateObject(TSource view)
          {
               TEntity entity = Mapper.Map<TEntity>(view);

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

/*
     using ( context )
     {
          var addedEntity = context.Entry( entity );
     
          addedEntity.State = EntityState.Added;
     
          context.Add( entity );
     
          context.SaveChanges();
     }
 * 
 * 
     using ( context )
     {
          var updatedEntity = context.Entry( entity );
          updatedEntity.State = EntityState.Modified;
          context.SaveChanges();
     
     }
*
               if (entityDto == null)
               {
                    throw new ArgumentNullException(string.Format(NullModelViewExceptionString, nameof(entityDto), methodName, className));
               }

              //if (entity == null)
               //{
               //     throw new InvalidOperationException(string.Format(NullEntityExceptionString, entityName, methodName, className));
               //}


               //string entityName = entity.GetType().Name;

               //if (entity == null)
               //{
               //     throw new InvalidOperationException(string.Format(NullEntityExceptionString, entityName, methodName, className));
               //}
          //public PayrollContext Context => this.context;
 */
