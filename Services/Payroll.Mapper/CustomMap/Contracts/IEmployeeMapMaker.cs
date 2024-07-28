using Payroll.Data;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap.Contracts
{
     public interface IEmployeeMapMaker
     {
          ICollection<GetEmployeeVM> EmployeeDtosMaker(PayrollContext db);

          ICollection<GetEmployeeVM> EmployeeDtosMaker( PayrollContext db, string name );

          GetEmployeeVM? SingleEmployeeDtoMaker( PayrollContext db,int empId );

          GetEmployeeVM? SingleEmployeeDtoMaker( PayrollContext db, string egnNumber );

          GetEmployeeVM? SingleEmployeeDtoMakerByListNumber( PayrollContext db, string numberFromTheList );
     }
}