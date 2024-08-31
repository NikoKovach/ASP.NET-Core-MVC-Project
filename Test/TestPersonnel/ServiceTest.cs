using AutoMapper;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.EmployeeViewModels;

namespace TestPersonnel.Demo
{
       public static class ServiceTest
       {
              public static void LinqTest( PayrollContext context )
              {
                     IRepository<Employee> repository = new Repository<Employee>( context );

                     var enyColl = repository.AllAsNoTracking()
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
                     } )
                     .ToList();


                     Console.WriteLine();
              }

              public static void TestCustomMapper( PayrollContext context )
              {
                     IRepository<Employee> repository = new Repository<Employee>( context );

                     //var result = new ProfileGetEmployeeVM()
                     //                     .Projection( repository.AllAsNoTracking() )
                     //                     .ToList();

                     var projections = new CustomProjections();
                     var result = (IQueryable<AllEmployeeVM>) projections
                            .EmployeeProjections[ "AllEmployees" ]( repository.AllAsNoTracking() );


                     Console.WriteLine( result.ToList()[ 0 ].FullName );
              }

              public static void TestValidate( PayrollContext context )
              {
                     IRepository<Employee> repository = new Repository<Employee>( context );

                     EmployeeVM viewModel = new EmployeeVM
                     {
                            PersonId = 6,
                            CompanyId = 10,
                            NumberFromTheList = "2",
                            IsPresent = true
                     };

                     //string? propertyName = nameof( viewModel.NumberFromTheList );

                     //var prop = viewModel.GetType().GetProperty( propertyName ).GetValue( viewModel ).ToString();

                     //var propValue = prop.GetValue( viewModel );
                     var service = new EmployeeVMValidate();
                     //string listNumber, int companyId,
                     service.NumberFromTheListIsValid( viewModel.NumberFromTheList, viewModel.CompanyId, repository );
              }

              public static void AutoMapperTest( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<Person> repository = new Repository<Person>( context );

                     var mapper = new MapEntity( autoMapper );

                     var persons = repository.AllAsNoTracking();

                     var personList = mapper.ProjectTo<Person, SearchPersonVM>( persons )
                                                             .OrderBy( c => c.FirstName )
                                                             .ToList();

                     Console.WriteLine( personList[ 0 ].ToString() );
              }
       }
}

/*
List<EmployeeBaseView> empViews = new List<EmployeeBaseView>()
                     {
                            new EmployeeBaseView(),
                            new GetEmpPersonView(),
                            new ContractView()
                     };

                     IProjectionTest baseProjection = new ProfileEmpBase();
                     IProjectionTest personProjection = new ProfileGetPerson();
                     IProjectionTest contractProjection = new ProfileGetEmpContract();

                     var profileList = new Dictionary<string,
                                                        Func<IQueryable<Employee>, IQueryable<EmployeeBaseView>>>
                     {
                            {"BaseProjection", baseProjection.ProjectionTest},

                            { "PersonProjection",personProjection.ProjectionTest },

                            { "ContractProjection",contractProjection.ProjectionTest }
                     };

                     var result = profileList[ "BaseProjection" ]( repository.AllAsNoTracking() );

                     var resultPerson = (IQueryable<GetEmpPersonView>)
                            profileList[ "PersonProjection" ]( repository.AllAsNoTracking() );

                     var contract = contractProjection.ProjectionTest( repository.AllAsNoTracking() ).ToList();
                     Console.WriteLine( resultPerson.FirstOrDefault().FullName );

*/

/*
 var enyColl = repository.AllAsNoTracking()
                     .Select( x => new ContractView
                     {
                            Id = x.Id,
                            FullName = x.Person.FullName,
                            Contract = new ContractSubView
                            {
                                   JobTitle = x.EmploymentContract.JobTitle,
                                   DepartmentName = x.EmploymentContract.Department.Name,
                                   ContractType = x.EmploymentContract.ContractType.Type,
                                   ContractNumber = x.EmploymentContract.ContractNumber,
                                   ContractDate = x.EmploymentContract.ContractDate.Date,
                                   LastAgreement = x.EmploymentContract.SupplementaryAgreements
                                          .Select( a => new ContractSubAnnexe
                                          {
                                                 AnnexId = a.Id,
                                                 JobTitle = a.JobTitle,
                                                 DepartmentName = a.Department.Name

                                          } )
                                            .OrderBy( x => x.AnnexId )
                                            .LastOrDefault()
                            }
                     } );
*/