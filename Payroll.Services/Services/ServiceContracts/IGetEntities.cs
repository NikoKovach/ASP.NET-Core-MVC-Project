
namespace Payroll.Services.Services.ServiceContracts
{
     public interface IGetEntities<TEntityView>
     {
          ICollection<TEntityView> GetAllEntities();

          ICollection<TEntityView> GetAllValidEntities();
          
     }
}
