//using Payroll.Data;

//using static Payroll.Services.Utilities.Messages.ExceptionMessages;

//namespace Payroll.Services.Services.AuthenticServices
//{
//     public class EmpConfirmation
//     {
//          /// <summary>
//          /// TODO : FOR TESTING
//          /// </summary>
//          /// 

//          public static void ValidateEmpId( PayrollContext context, int employeeId )
//          {
//               if ( !EmployeeIdIsValid(context,employeeId ))
//               {
//                    string exceptionMessage = string.Format(InvalidEmployeeId,nameof(employeeId));

//                    throw new ArgumentException(exceptionMessage,nameof(employeeId));
//               }
//          }

//          private static bool EmployeeIdIsValid( PayrollContext db, int id )
//          {
//               int? empId = db.Employees
//                    .Where(x => x.Id == id)
//                    .Select(y => y.Id)
//                    .FirstOrDefault();

//               if ( empId == null || empId == 0 )
//               {
//                    return false;
//               }

//               return true;
//          } 
//     }
//}
