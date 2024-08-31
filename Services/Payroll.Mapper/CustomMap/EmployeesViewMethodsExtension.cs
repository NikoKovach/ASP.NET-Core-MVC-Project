using Microsoft.EntityFrameworkCore;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
       public static class EmployeesViewMethodsExtension
       {

              //public static IQueryable<AllEmployeeVM>? AllEmployeeVMProjection
              //                                                                       ( this IQueryable<Employee>? employees )
              //{
              //       IQueryable<AllEmployeeVM>? result = employees.Select( x => new AllEmployeeVM
              //       {
              //              Id = x.Id,
              //              NumberFromTheList = x.NumberFromTheList,
              //              FullName = $"{x.Person.FirstName} {x.Person.MiddleName} {x.Person.LastName}",
              //              EGN = x.Person.EGN,
              //              DepartmentName = x.EmploymentContract.Department.Name,
              //              JobTitle = x.EmploymentContract.JobTitle,
              //              ContractNumber = x.EmploymentContract.ContractNumber,
              //              ContractDate = x.EmploymentContract.ContractDate.ToShortDateString()
              //       } );

              //       return result;
              //}

              public static IQueryable<GetEmployeeVM> GetEmployeeVMProjection
                                                                                                  ( this IQueryable<Employee>? employees )
              {
                     IQueryable<GetEmployeeVM>? empoyeeCollection = employees
                     .Select( x => new GetEmployeeVM
                     {
                            Id = x.Id,
                            CompanyId = x.CompanyId,
                            NumberFromTheList = x.NumberFromTheList,
                            IsPresent = x.IsPresent,
                            Person = new PersonEmpVM
                            {
                                   PersonId = x.Person.Id,
                                   FullName = ( x.Person.FirstName + " " +
                                          ( x.Person.MiddleName ?? "" ) + " " + x.Person.LastName ) ?? "",
                                   GenderId = x.Person.GenderId,
                                   GenderType = x.Person.Gender.Type,
                                   EGN = x.Person.EGN,
                                   PhotoFilePath = x.Person.PhotoFilePath,
                                   PermanentAddress = new AddressEmpVM
                                   {
                                          Country = x.Person.PermanentAddress.Country,
                                          Region = x.Person.PermanentAddress.Region,
                                          Municipality = x.Person.PermanentAddress.Municipality,
                                          City = x.Person.PermanentAddress.City,
                                          Street = x.Person.PermanentAddress.Street,
                                          Number = x.Person.PermanentAddress.Number,
                                          Entrance = x.Person.PermanentAddress.Entrance,
                                   }.ToString() ?? "",
                                   CurrentAddress = new AddressEmpVM
                                   {
                                          Country = x.Person.CurrentAddress.Country,
                                          Region = x.Person.CurrentAddress.Region,
                                          Municipality = x.Person.CurrentAddress.Municipality,
                                          City = x.Person.CurrentAddress.City,
                                          Street = x.Person.CurrentAddress.Street,
                                          Number = x.Person.CurrentAddress.Number,
                                          Entrance = x.Person.CurrentAddress.Entrance,
                                   }.ToString() ?? "",
                            },
                            ContactInfo = new ContactsEmpVM
                            {
                                   PhoneNumberOne = x.Person.ContactInfoList
                                                               .OrderBy( x => x.Id )
                                                               .LastOrDefault().PhoneNumberOne,
                                   PhoneNumberTwo = x.Person.ContactInfoList
                                                               .OrderBy( x => x.Id )
                                                               .LastOrDefault().PhoneNumberTwo,
                                   E_MailAddress1 = x.Person.ContactInfoList
                                                               .OrderBy( x => x.Id )
                                                               .LastOrDefault().E_MailAddress1,
                                   Website = x.Person.ContactInfoList
                                                               .OrderBy( x => x.Id )
                                                               .LastOrDefault().WebSite,
                            },
                            IdCardPassport = new IdDocumentEmpVM
                            {
                                   DocumentName = x.Person.IdDocuments
                                                                      .OrderBy( x => x.Id )
                                                                      .LastOrDefault()
                                                                      .DocumentType
                                                                      .DocumentName,
                                   DocumentNumber = x.Person.IdDocuments
                                                                      .OrderBy( x => x.Id )
                                                                      .LastOrDefault()
                                                                      .DocumentNumber,
                            },
                            ContractInfo = new ContractEmpVM
                            {
                                   //JobTitle = x.EmploymentContract.SupplementaryAgreements.LastOrDefault().JobTitle,
                                   JobTitle = x.EmploymentContract.JobTitle,
                                   DepartmentName = x.EmploymentContract.Department.Name,
                                   ContractType = x.EmploymentContract.ContractType.Type,
                                   ContractNumber = x.EmploymentContract.ContractNumber,
                                   ContractDate = x.EmploymentContract.ContractDate.ToShortDateString(),
                                   AgreementsCount = x.EmploymentContract.SupplementaryAgreements.Count,
                            }
                     } )
                      .OrderBy( x => x.Person.FullName );

                     return empoyeeCollection;
              }

              public static async Task GetInfoFromAnnexesAsync
                     ( this List<GetEmployeeVM> empoyeeCollection, IQueryable<Employee> employees )
              {
                     foreach ( var item in empoyeeCollection )
                     {
                            if ( item.ContractInfo.AgreementsCount > 0 )
                            {
                                   var subAnnex = await employees
                                                         .Where( x => x.Id == item.Id )
                                                         .Select( x => new AnnexJobTitleVM
                                                         {
                                                                Id = x.EmploymentContract.SupplementaryAgreements
                                                                                     .OrderBy( x => x.Id ).LastOrDefault().Id,
                                                                JobTitle = x.EmploymentContract.SupplementaryAgreements
                                                                                     .OrderBy( x => x.Id ).LastOrDefault().JobTitle,
                                                                DepartmentName = x.EmploymentContract.SupplementaryAgreements
                                                                                      .OrderBy( x => x.Id ).LastOrDefault().Department.Name
                                                         } )
                                                         .FirstOrDefaultAsync();

                                   if ( !string.IsNullOrEmpty( subAnnex.JobTitle ) )
                                          item.ContractInfo.JobTitle = subAnnex.JobTitle;

                                   if ( !string.IsNullOrEmpty( subAnnex.DepartmentName ) )
                                          item.ContractInfo.DepartmentName = subAnnex.DepartmentName;
                            }
                     }
              }
       }
}
