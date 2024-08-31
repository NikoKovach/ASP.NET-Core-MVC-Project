using Payroll.Data.Common;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class EmployeeVMValidate : IValidateEmployeeVModels
       {
              public EmployeeVMValidate()
              {
                     this.EntityState = new ViewModelState();
              }

              public ViewModelState EntityState { get; }

              public void NumberFromTheListIsValid( string listNumber, int companyId, IRepository<Employee> repository )
              {
                     bool isNumber = int.TryParse( listNumber, out int numberFromTheList );

                     if ( !isNumber )
                     {
                            this.EntityState.ModelIsValid = false;
                            this.EntityState.ErrorMessage =
                                   string.Format( OutputMessages.ErrorNotANumber );

                            return;
                     }

                     if ( numberFromTheList < 1 )
                     {
                            this.EntityState.ModelIsValid = false;
                            this.EntityState.ErrorMessage =
                                   string.Format( OutputMessages.ErrorNumberIsNegative );

                            return;
                     }

                     List<int>? empPropertyList = repository
                                                                        .AllAsNoTracking()
                                                                        .Where( x => x.CompanyId == companyId )
                                                                        .Select( x => int.Parse( x.NumberFromTheList ) )
                                                                        .ToList();

                     if ( empPropertyList.Contains( numberFromTheList ) )
                     {
                            this.EntityState.ModelIsValid = false;
                            this.EntityState.ErrorMessage = string.Format( OutputMessages.ErrorValueExists,
                                                                                                                                     numberFromTheList );
                     }
                     else
                     {
                            this.EntityState.ModelIsValid = true;
                            this.EntityState.ErrorMessage = string.Empty;
                     }
              }
       }
}
