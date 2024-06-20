
namespace Payroll.Services.Services.ServiceContracts
{
     public interface IGetEntities<TEntityView>
     {
          Task<ICollection<TEntityView>> GetAllEntitiesAsync();

          Task<ICollection<TEntityView>> GetAllValidEntitiesAsync();
          
     }
}
