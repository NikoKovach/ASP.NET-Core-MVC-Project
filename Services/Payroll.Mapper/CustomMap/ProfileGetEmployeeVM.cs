using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
       public class ProfileGetEmployeeVM : IProjection
       {
              public IQueryable<BaseEmployeeVM> Projection( IQueryable<Employee> employees )
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
                                   FullName = x.Person.FullName,
                                   GenderId = x.Person.GenderId,
                                   GenderType = x.Person.Gender.Type,
                                   EGN = x.Person.EGN,
                                   PhotoFilePath = x.Person.PhotoFilePath,
                                   PermanentAddress = x.Person.PermanentAddress.GetAddress,
                                   CurrentAddress = x.Person.CurrentAddress.GetAddress,
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
                                   JobTitle = x.EmploymentContract.JobTitle,
                                   DepartmentName = x.EmploymentContract.Department.Name,
                                   ContractType = x.EmploymentContract.ContractType.Type,
                                   ContractNumber = x.EmploymentContract.ContractNumber,
                                   ContractDate = x.EmploymentContract.ContractDate.ToShortDateString(),
                                   LastAnnex = x.EmploymentContract.SupplementaryAgreements
                                                                              .Select( a => new AnnexJobTitleVM
                                                                              {
                                                                                     Id = a.Id,
                                                                                     JobTitle = a.JobTitle,
                                                                                     DepartmentName = a.Department.Name

                                                                              } )
                                                                                .OrderBy( x => x.Id )
                                                                                .LastOrDefault()
                            }
                     } );

                     return empoyeeCollection;
              }

       }
}
