using Payroll.Data;
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Mapper.CustomMap.Contracts
{
     public interface IEmployeeMapMaker
     {
          ICollection<GetEmployeeDto> EmployeeDtosMaker(PayrollContext db);

          ICollection<GetEmployeeDto> EmployeeDtosMaker( PayrollContext db, string name );

          GetEmployeeDto? SingleEmployeeDtoMaker( PayrollContext db,int empId );

          GetEmployeeDto? SingleEmployeeDtoMaker( PayrollContext db, string egnNumber );

          GetEmployeeDto? SingleEmployeeDtoMakerByListNumber( PayrollContext db, string numberFromTheList );
     }
}