namespace Payroll.Services.Services.ServiceContracts
{
       public interface IBasicDelete
       {
              Task DeleteAsync( int? entityId, int? parentEntityId = null );
       }
}
