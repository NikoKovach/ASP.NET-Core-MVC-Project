namespace Payroll.Services.Services.ServiceContracts
{
       public interface IBasicGetEntity<TEntityView>
       {
              Task<TEntityView?> GetEntityAsync( int? entityId );
       }
}
