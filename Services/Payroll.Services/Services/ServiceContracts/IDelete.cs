namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDelete
       {
              Task DeleteAsync( int? entityId, int? parentEntityId = null );
       }
}
