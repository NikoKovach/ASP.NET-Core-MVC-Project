using AutoMapper;
using Payroll.Data;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface ICreateUpdateEntity<TSource, TEntity>
         where TEntity : class
         where TSource : class
     {
          TEntity CreateEntity(TSource view);

          TEntity UpdateEntity(TSource view);

          public PayrollContext Context { get ; }

          public IMapper Mapper { get ;  }
     }
}
