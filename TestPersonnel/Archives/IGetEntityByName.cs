namespace Payroll.Services.Services.ServiceContracts
{
     public interface IGetEntityByName<TEntityView>
     {
          TEntityView GetEntityByName( string name );
     }
}
