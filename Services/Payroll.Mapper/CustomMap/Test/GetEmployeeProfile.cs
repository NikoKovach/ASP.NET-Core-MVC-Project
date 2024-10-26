//using Payroll.Models;
//using Payroll.ViewModels.EmployeeViewModels;

//namespace Payroll.Mapper.CustomMap.Test
//{
//       public class GetEmployeeProfile
//       {
//              public IQueryable<GetEmployeeVM>? CreateProjection( IQueryable<Employee> employees )
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
//                                   FullName = x.Person.FirstName + " " +
//                                          ( x.Person.MiddleName ?? "" ) + " " + x.Person.LastName ?? "",
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
//                            ContractInfo = new ContractEmpVM()
//                     } )
//                      .OrderBy( x => x.Person.FullName );

//                     return empoyeeCollection;
//              }
//       }
//}
