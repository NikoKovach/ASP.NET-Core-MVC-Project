using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface ISearchEmployee
     {
          GetEmployeeDto GetEmployeeByEGN( string egnNumber );

          GetEmployeeDto GetEmployeeByListNumber( string numberFromTheList );

          ICollection<GetEmployeeDto> GetEmployeeByName( string name );
     }
}
