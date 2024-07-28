using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
     public interface ISearchEmployee
     {
          GetEmployeeVM GetEmployeeByEGN( string egnNumber );

          GetEmployeeVM GetEmployeeByListNumber( string numberFromTheList );

          ICollection<GetEmployeeVM> GetEmployeeByName( string name );
     }
}
