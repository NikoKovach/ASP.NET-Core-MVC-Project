using Payroll.Data;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface IGetEntityById<TEntityView>
     {
          TEntityView GetEntityById( int entityId );
     }
}
