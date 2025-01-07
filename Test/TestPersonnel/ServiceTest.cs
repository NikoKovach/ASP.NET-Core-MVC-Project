using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.UtilitiesServices;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels.CustomValidation;
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace TestPersonnel.Demo
{
       public static class ServiceTest
       {
              public static void LinqTest( PayrollContext context )
              {
                     IRepository<Employee> repository = new Repository<Employee>( context );
                     int personId = 6;
                     var employee = repository.All()
                                                                   .Where( x => x.Person.Id == personId ).Include( i => i.Person )
                                                                   .FirstOrDefault();

                     context.Entry( employee ).State = EntityState.Detached;

                     //string revativePath2 = @"/app-folder/Булгарстрой-World-АД/Employees/Gocho-B-Petrov/man.png";
                     string relativePath = "new image";

                     employee.Person.PhotoFilePath = relativePath;

                     repository.Update( employee );

                     repository.SaveChangesAsync().GetAwaiter().GetResult();

                     Console.WriteLine();
              }

              public static void TestCustomMapper( PayrollContext context )
              {
                     //IRepository<Employee> repository = new Repository<Employee>( context );

                     ////var result = new ProfileGetEmployeeVM()
                     ////                     .Projection( repository.AllAsNoTracking() )
                     ////                     .ToList();

                     //var projections = new CustomProjections();
                     //var result = (IQueryable<AllEmployeeVM>) projections
                     //       .EmployeeProjections[ "AllEmployees" ]( repository.AllAsNoTracking() );


                     //Console.WriteLine( result.ToList()[ 0 ].FullName );
              }

              public static void TestValidate( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<Diploma> repository = new Repository<Diploma>( context );
                     var mapEntity = new MapEntity( autoMapper );

                     //var restrictionsFactory = new RestrictionsFactory();
                     //var customProjections = new CustomProjections();
                     //var empService = new EmployeeService( repository, mapEntity, customProjections );
                     //***************************************************************

                     //************************************************************

                     DiplomaVM viewModel = new DiplomaVM
                     {
                            PersonId = 7,
                            Seria = "qwqw"
                     };

                     var modelState = new ModelStateDictionary();
                     var validateService = new ValidateDiplomaVMService();
                     validateService.Validate( modelState, viewModel );
                     //validateService.RenameInvalidValueErrorMsg( modelState, viewModel );
              }

              public static void AutoMapperTest( PayrollContext context, IMapper autoMapper )
              {
                     var genericMapper = new MapEntity( autoMapper );

                     IRepository<Employee> repository = new Repository<Employee>( context );

                     var empList = repository.AllAsNoTracking().Where( x => x.CompanyId == 5 );

                     var result = autoMapper.ProjectTo<SearchEmployeeVM>( empList ).ToList();

                     var resultGenericMapper = genericMapper
                                   .ProjectTo<Employee, SearchEmployeeVM>( empList ).ToList();

                     Console.WriteLine();
              }

              public static void PersonPartTest( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<Person> repo = new Repository<Person>( context );
                     var mapper = new MapEntity( autoMapper );
                     var personsFactory = new FactoryPersonsCollection( mapper, repo );

                     var filter = new PersonFilterVM
                     {
                            SearchName = "asen",
                            CivilID = "870",

                     };

                     personsFactory.SortedCollection( "", filter );
                     //var firstPerson = repo.AllAsNoTracking().FirstOrDefault();
                     //var mappedPerson = mapper.Map<Person, SearchPersonVM>( firstPerson );

                     //var searchPersons = mapper
                     //       .ProjectTo<Person, SearchPersonVM>( repo.AllAsNoTracking() )
                     //       .OrderBy( x => x.FirstName )
                     //       .ThenBy( x => x.LastName ).ToList();

                     //var personService = new PersonService( repo, mapper, personsFactory );

                     //var result = personService.AllActive_SearchPersonVM().ToList();
                     //var personVM = new PersonVM
                     //{
                     //       Id = 7,
                     //       FirstName = "Costa",
                     //       LastName = "Costadinov",
                     //       CivilNumber = "8810251234",
                     //       GenderType = "man",
                     //};

                     //var person = mapper.Map<PersonVM, Person>( personVM );

                     Console.WriteLine();
              }

              public static void AddressValidateTest( PayrollContext context, IMapper autoMapper )
              {
                     var contractVM = new LaborAgreementVM
                     {
                            AdditionalPaidAnnualLeaveInDays = 5,
                            CompanyId = 5,
                            ContractDate = DateTime.Parse( "5.01.2025" ),
                            ContractNumber = "5555555555",
                            ContractTypeId = 1,
                            DateOfReceipt = DateTime.Parse( "5.01.2025" ),
                            DepartmentId = 3,
                            EAC = "Something",
                            EmployeeId = 13,
                            FirstLastName = null,
                            FirstName = null,
                            HasBeenDeleted = null,
                            Id = null,
                            IsActive = true,
                            IsNegotiatedInFavorOf = null,
                            IsRegistered = true,
                            JobTitle = "Accountant",
                            LaborCodeArticle = null,
                            LaborCodeArticleId = 1,
                            LastName = null,
                            NCOP = "7001-2325",
                            NoticePeriodInDays = 30,
                            PaidLeaveInDays = 25,
                            PercentSWE = 0.06,
                            PlaceId = 37,
                            ProbationInMonths = 6,
                            ReceivedAJobDescription = null,
                            Salary = 4000,
                            SalaryDayOfTheMonth = 20,
                            SpecialtyWorkExperience = null,
                            StartingWorkDate = DateTime.Parse( "5.01.2025" ),
                            TrialPeriod = 30,
                            WorkExperience = null,
                            WorkPlaceId = 37,
                            WorkTime = 8,
                     };

                     //var addresses = context.Addresses.AsNoTracking();

                     var contract = autoMapper.Map<EmploymentContract>( contractVM );

                     context.EmploymentContracts.Add( contract );
                     context.SaveChanges();
                     //var stringList = defaultCollection.Select( x => x.ToString() ).ToList();


                     Console.WriteLine( contract.Id );
              }

              public static void AnyServiceTest( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<Address> repository = new Repository<Address>( context );
                     var mapEntity = new MapEntity( autoMapper );

                     int personId = 17;

                     var personPermanentA = repository.AllAsNoTracking()
                                                                      .Where( x => x.PersonPermanentAddresses
                                                                                                 .Any( y => y.Id == personId ) )
                                                                      .Include( x => x.PersonPermanentAddresses )
                                                                      .ToList();

                     var personCurrentA = repository.AllAsNoTracking()
                                                                      .Where( x => x.PersonCurrentAddresses
                                                                                                 .Any( y => y.Id == personId ) )
                                                                      .Include( x => x.PersonCurrentAddresses )
                                                                      .ToList();

                     Console.WriteLine();

                     //var result = mapEntity.Map<List<PersonVM>, List<Person>>( entitiesForEdit );
                     //Console.WriteLine( nameof( filterVM.CivilID ) );
                     //string? sort = "LastName_desc";
                     //var personsFactory = new PersonsCollectionFactory( mapEntity, repository.AllAsNoTracking() );
                     //personsFactory.Filtrate( filterVM, sort );
              }

              public static void PropertyOrderReflection()
              {
                     ContactInfoVM firstContact = new ContactInfoVM
                     {
                            Id = 20,
                            PersonId = 7,
                            PhoneNumberOne = "123",
                            PhoneNumberTwo = "456",
                            E_MailAddress1 = "E-1",
                            E_MailAddress2 = "e-2",
                            WebSite = "Website",
                            HasBeenDeleted = false
                     };

                     var propList = firstContact.GetType()
                              .GetProperties()
                              .OrderBy( f => (int?) ( f.CustomAttributes.Where( a => a.AttributeType == typeof( OrderAttribute ) )
                                                                                                    .FirstOrDefault()?.ConstructorArguments[ 0 ].Value ) ?? -1 )
                              .Select( x => x.Name )
                              .ToList();

                     Console.WriteLine();

              }

              public static void ConfigurationTest()
              {
                     //D:\SoftUni Courses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Test\
                     //TestPersonnel\bin\Debug\net8.0
                     //var dir = Environment.CurrentDirectory;
                     //var files = Directory.GetFiles( path );
                     //string path = Path.GetFullPath( Environment.CurrentDirectory + @"\..\..\..\" );

                     //string dirPath = Path.GetFullPath( Environment.CurrentDirectory + @"\..\..\..\" );
                     //string jsonFileName = "appsettingsSecond.json";

                     //var jsonPath = dirPath + jsonFileName;

                     //IConfigurationRoot? config = new ConfigurationBuilder().AddJsonFile( jsonPath, true, true ).Build();

                     //var pageSize = config[ "Paging:PageSize" ];
                     //var pSection = config.GetSection( "Paging:PageSize" ).Value;


                     Console.WriteLine();
              }

              public static void PerformanceTest( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<EmploymentContract> repository = new Repository<EmploymentContract>( context );

                     var vslidateService = new ValidateServiceStringValueExists( repository );

                     var modelState = new ModelStateDictionary();
                     var departmentVM = new DepartmentVM
                     {
                            DepartmentId = 1,
                            Name = "General Staff",//"Innovations", // "Sales",
                     };

                     object[] checkDictionaryParams =
                     [ nameof( DepartmentVM ), nameof( departmentVM.Name ), departmentVM.Name ];

                     vslidateService.Validate( modelState, departmentVM, "", checkDictionaryParams );

                     Console.WriteLine();


              }
       }
}



/*

 List<PersonVM>? entitiesForEdit = new List<PersonVM>()
                     {
                            new PersonVM
                            {
                                   Id = 3,
                                   FirstName = "F one",
                                   LastName = "L one",
                                   CivilNumber = "1111",
                                   GenderType = "man",
                            },
                            new PersonVM
                            {
                                   Id = 3,
                                   FirstName = "F two",
                                   LastName = "L two",
                                   CivilNumber = "2222",
                                   GenderType = "man",
                            },
                            new PersonVM
                            {
                                   Id = 3,
                                   FirstName = "F five",
                                   LastName = "L five",
                                   CivilNumber = "3333",
                                   GenderType = "man",
                            },
                     };
                    appFolderPath = 
                    D:\SoftUni Courses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Web\PersonnelWebApp\AppFolder

                     //string appFolderPath =
                    //@" D:\SoftUni Courses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Web\PersonnelWebApp\AppFolder";

                    //int personId = 6;
                    //int companyId = 10;


                    //empService.CreateEmployeeFolderAsync( appFolderPath, personId, companyId )
                    //                     .GetAwaiter()
                    //                     .GetResult();

                    //string empFolder = @"D:\SoftUni Courses\A Exercises\AA Git Projects\"
                    //                                   + @"ASP.NET-Core-MVC-Payroll Web Project\Web\PersonnelWebApp\AppFolder"
                    //                                   + @"\Булгарстрой-World-АД\Employees\Gocho-B-Petrov";

                    //var relativeFolder = EnvironmentService.CreateRelativePath( empFolder, "/app-folder", "AppFolder" );

                    //var file = @"D:\NK_Pictures\Arrioch-Elements-Leaf.ico";
                    //var file = @"D:\NK_Pictures\emp-3.jpg"; //
                    //var file = @"D:\SoftUni Courses\A Exercises\AA Git Projects\Images\DSC_5605.jpg";

                    //using var stream = new MemoryStream( File.ReadAllBytes( file ).ToArray() );
                    //var formFile = new FormFile( stream, 0, stream.Length, "streamFile", file.Split( @"\" ).Last() );


//var laborVM = new LaborAgreementVM
                     //{
                     //       Id = 5,
                     //       IsActive = true,
                     //       ContractType = "Labor Contract",
                     //       ContractNumber = "123456",
                     //       ContractDate = DateTime.Parse( "10.10.2024 " ),
                     //       WorkExperience = null,
                     //       SpecialtyWorkExperience = null,
                     //       LaborCodeArticle = "chl.67",
                     //       TrialPeriod = null,
                     //       IsNegotiatedInFavorOf = null,
                     //       JobTitle = "Accountant",
                     //       DepartmentName = "Accountig",
                     //       EAC = null,
                     //       NCOP = null,
                     //       WorkTime = 8,
                     //       Salary = 3000.00m,
                     //       PercentSWE = 0.06,
                     //       SalaryDayOfTheMonth = 22,
                     //       PaidLeaveInDays = 25,
                     //       AdditionalPaidAnnualLeaveInDays = null,
                     //       ProbationInMonths = 3,
                     //       NoticePeriodInDays = 60,
                     //       ReceivedAJobDescription = true,
                     //       StartingWorkDate = DateTime.Parse( "1.12.2024 " ),
                     //       DateOfReceipt = DateTime.Parse( "1.12.2024" ),
                     //       EmployeeId = 12,
                     //       PlaceId = 22,
                     //       WorkPlaceId = 22,
                     //       HasBeenDeleted = false,
                     //};

                     //var mapSesult = genericMapper.Map<LaborAgreementVM, EmploymentContract>( laborVM );
*/