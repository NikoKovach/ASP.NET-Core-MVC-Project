using AutoMapper;
using Payroll.Data;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface IAddUpdateEntity 
     {
          Task AddEntityAsync<TEntity,TSource>(TSource entityDto)  ;

          Task UpdateEntityAsync<TEntity,TSource>(TSource entityDto);

     }
}
