
namespace Payroll.Services.Services.ServiceContracts
{
     public interface IGetEntitiesByEmployeeId<TEntityDto>
     {
          ICollection<TEntityDto> GetAllEntitiesByEmployeeId(int employeeId);

          ICollection<TEntityDto> GetValidEntitiesByEmployeeId( int employeeId );

          //TEntityView GetValidEntity( int employeeId );
     }
}
