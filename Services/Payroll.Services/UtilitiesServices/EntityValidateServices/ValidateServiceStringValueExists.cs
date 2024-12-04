using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Models;
using Payroll.ViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateServiceStringValueExists : ValidateBaseClass, IValidate<ValidateBaseModel>
       {
              private IRepository<EmploymentContract> repository;
              private IDictionary<string, Func<string, string, Task>> checkDictionary;

              public ValidateServiceStringValueExists( IRepository<EmploymentContract> repository )
              {
                     this.repository = repository;

                     SetDictionary();
              }

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                 [CallerMemberName] string actionName = "", params object[] parameters )
              {
                     base.ModelState = modelState;

                     if ( parameters.Length < 3 )
                            return;

                     string viewModelType = (string) parameters[ 0 ];
                     string viewModelPropertyName = (string) parameters[ 1 ];
                     string stringPropertyValue = (string) parameters[ 2 ];

                     string key = $"{viewModelType}{viewModelPropertyName}"; // string key = "DepartmentVMName";

                     if ( this.checkDictionary.ContainsKey( key ) && !string.IsNullOrEmpty( stringPropertyValue ) )
                     {
                            this.checkDictionary[ key ]( viewModelPropertyName, stringPropertyValue ).GetAwaiter().GetResult();
                     }
              }

              //#####################################################################

              private void SetDictionary()
              {
                     this.checkDictionary = new Dictionary<string, Func<string, string, Task>>();

                     this.checkDictionary[ "DepartmentVMName" ] = DepartmentNameExists;

                     this.checkDictionary[ "LaborCodeArticleVMArticle" ] = LaborCodeArticleExists;

                     this.checkDictionary[ "AgreementTypeVMType" ] = AgreementTypeExists;
              }

              private async Task DepartmentNameExists( string propertyName, string propertyValue )
              {
                     string? departmentName = await this.repository.Context.Departments
                                                                      .Where( x => x.Name == propertyValue )
                                                                      .Select( x => x.Name )
                                                                      .FirstOrDefaultAsync();

                     this.SetModelStateError( departmentName, propertyName, propertyValue );
              }

              private async Task LaborCodeArticleExists( string propertyName, string propertyValue )
              {
                     string? laborCodeArticle = await this.repository.Context.LaborCodeArticles
                                                                      .Where( x => x.Article == propertyValue )
                                                                      .Select( x => x.Article )
                                                                      .FirstOrDefaultAsync();

                     this.SetModelStateError( laborCodeArticle, propertyName, propertyValue );
              }

              private async Task AgreementTypeExists( string propertyName, string propertyValue )
              {
                     string? agreementType = await this.repository.Context.ContractTypes
                                                                      .Where( x => x.Type == propertyValue )
                                                                      .Select( x => x.Type )
                                                                      .FirstOrDefaultAsync();

                     this.SetModelStateError( agreementType, propertyName, propertyValue );
              }

              private void SetModelStateError( string? dbValue, string propertyName, string propertyValue )
              {
                     if ( !string.IsNullOrEmpty( dbValue ) )
                     {
                            string errorMessage = $"Property : {propertyName} with value : ' {propertyValue} ' allready exists  !";

                            this.ModelState.AddModelError( propertyName, errorMessage );
                     }
              }
       }
}

