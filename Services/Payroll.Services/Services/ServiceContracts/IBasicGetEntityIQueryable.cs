namespace Payroll.Services.Services.ServiceContracts
{
       public interface IBasicGetEntityIQueryable<TResult>
       {
              IQueryable<TResult>? GetEntity( int? entityId );
       }
}
