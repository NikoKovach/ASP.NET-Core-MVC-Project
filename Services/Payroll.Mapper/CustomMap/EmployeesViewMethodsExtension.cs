//using Microsoft.EntityFrameworkCore;
//using Payroll.Models;
//using Payroll.ViewModels.EmployeeViewModels;

//namespace Payroll.Mapper.CustomMap
//{
//       public static class EmployeesViewMethodsExtension
//       {

//              public static IQueryable<GetEmployeeVM> GetEmployeeVMProjection
//                                                                                                  ( this IQueryable<Employee>? employees )
//              {
//                     IQueryable<GetEmployeeVM>? empoyeeCollection = employees
//                     .Select( x => new GetEmployeeVM
//                     {
//                            Id = x.Id,
//                            CompanyId = x.CompanyId,
//                            NumberFromTheList = x.NumberFromTheList,
//                            IsPresent = x.IsPresent,
//                            Person = new PersonEmpVM
//                            {
//                                   PersonId = x.Person.Id,
//                                   FullName = ( x.Person.FirstName + " " +
//                                          ( x.Person.MiddleName ?? "" ) + " " + x.Person.LastName ) ?? "",
//                                   GenderId = x.Person.GenderId,
//                                   GenderType = x.Person.Gender.Type,
//                                   EGN = x.Person.EGN,
//                                   PhotoFilePath = x.Person.PhotoFilePath,
//                                   PermanentModel = new ModelEmpVM
//                                   {
//                                          Country = x.Person.PermanentModel.Country,
//                                          Region = x.Person.PermanentModel.Region,
//                                          Municipality = x.Person.PermanentModel.Municipality,
//                                          City = x.Person.PermanentModel.City,
//                                          Street = x.Person.PermanentModel.Street,
//                                          Number = x.Person.PermanentModel.Number,
//                                          Entrance = x.Person.PermanentModel.Entrance,
//                                   }.ToString() ?? "",
//                                   CurrentModel = new ModelEmpVM
//                                   {
//                                          Country = x.Person.CurrentModel.Country,
//                                          Region = x.Person.CurrentModel.Region,
//                                          Municipality = x.Person.CurrentModel.Municipality,
//                                          City = x.Person.CurrentModel.City,
//                                          Street = x.Person.CurrentModel.Street,
//                                          Number = x.Person.CurrentModel.Number,
//                                          Entrance = x.Person.CurrentModel.Entrance,
//                                   }.ToString() ?? "",
//                            },
//                            ContactInfo = new ContactsEmpVM
//                            {
//                                   PhoneNumberOne = x.Person.ContactInfoList
//                                                               .OrderBy( x => x.Id )
//                                                               .LastOrDefault().PhoneNumberOne,
//                                   PhoneNumberTwo = x.Person.ContactInfoList
//                                                               .OrderBy( x => x.Id )
//                                                               .LastOrDefault().PhoneNumberTwo,
//                                   E_MailModel1 = x.Person.ContactInfoList
//                                                               .OrderBy( x => x.Id )
//                                                               .LastOrDefault().E_MailModel1,
//                                   Website = x.Person.ContactInfoList
//                                                               .OrderBy( x => x.Id )
//                                                               .LastOrDefault().WebSite,
//                            },
//                            IdCardPassport = new IdDocumentEmpVM
//                            {
//                                   DocumentName = x.Person.IdDocuments
//                                                                      .OrderBy( x => x.Id )
//                                                                      .LastOrDefault()
//                                                                      .DocumentType
//                                                                      .DocumentName,
//                                   DocumentNumber = x.Person.IdDocuments
//                                                                      .OrderBy( x => x.Id )
//                                                                      .LastOrDefault()
//                                                                      .DocumentNumber,
//                            },
//                            ContractInfo = new ContractEmpVM
//                            {
//                                   //JobTitle = x.EmploymentContract.SupplementaryAgreements.LastOrDefault().JobTitle,
//                                   JobTitle = x.EmploymentContract.JobTitle,
//                                   DepartmentName = x.EmploymentContract.Department.Name,
//                                   ContractType = x.EmploymentContract.ContractType.Type,
//                                   ContractNumber = x.EmploymentContract.ContractNumber,
//                                   ContractDate = x.EmploymentContract.ContractDate.ToShortDateString(),
//                                   AgreementsCount = x.EmploymentContract.SupplementaryAgreements.Count,
//                            }
//                     } )
//                      .OrderBy( x => x.Person.FullName );

//                     return empoyeeCollection;
//              }

//              public static async Task GetInfoFromAnnexesAsync
//                     ( this List<GetEmployeeVM> empoyeeCollection, IQueryable<Employee> employees )
//              {
//                     foreach ( var item in empoyeeCollection )
//                     {
//                            if ( item.ContractInfo.AgreementsCount > 0 )
//                            {
//                                   var subAnnex = await employees
//                                                         .Where( x => x.Id == item.Id )
//                                                         .Select( x => new AnnexJobTitleVM
//                                                         {
//                                                                Id = x.EmploymentContract.SupplementaryAgreements
//                                                                                     .OrderBy( x => x.Id ).LastOrDefault().Id,
//                                                                JobTitle = x.EmploymentContract.SupplementaryAgreements
//                                                                                     .OrderBy( x => x.Id ).LastOrDefault().JobTitle,
//                                                                DepartmentName = x.EmploymentContract.SupplementaryAgreements
//                                                                                      .OrderBy( x => x.Id ).LastOrDefault().Department.Name
//                                                         } )
//                                                         .FirstOrDefaultAsync();

//                                   if ( !string.IsNullOrEmpty( subAnnex.JobTitle ) )
//                                          item.ContractInfo.JobTitle = subAnnex.JobTitle;

//                                   if ( !string.IsNullOrEmpty( subAnnex.DepartmentName ) )
//                                          item.ContractInfo.DepartmentName = subAnnex.DepartmentName;
//                            }
//                     }
//              }
//       }
//}
