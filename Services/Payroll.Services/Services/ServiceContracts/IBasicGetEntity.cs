namespace Payroll.Services.Services.ServiceContracts
{
       public interface IBasicGetEntity
       {
              IQueryable<TResult>? GetEntity<TResult>( int? entityId );
       }
}
