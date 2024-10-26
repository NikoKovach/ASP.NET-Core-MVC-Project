using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.Services;
using Payroll.Services.UtilitiesServices;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels.CustomValidation;
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
                     IRepository<Employee> repository = new Repository<Employee>( context );

                     //var result = new ProfileGetEmployeeVM()
                     //                     .Projection( repository.AllAsNoTracking() )
                     //                     .ToList();

                     var projections = new CustomProjections();
                     var result = (IQueryable<AllEmployeeVM>) projections
                            .EmployeeProjections[ "AllEmployees" ]( repository.AllAsNoTracking() );


                     Console.WriteLine( result.ToList()[ 0 ].FullName );
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
                     IRepository<Person> repository = new Repository<Person>( context );

                     var mapper = new MapEntity( autoMapper );

                     var persons = repository.AllAsNoTracking();

                     var personList = mapper.ProjectTo<Person, SearchPersonVM>( persons )
                                                             .OrderBy( c => c.FirstName )
                                                             .ToList();

                     Console.WriteLine( personList[ 0 ].ToString() );
              }

              public static void PersonPartTest( PayrollContext context, IMapper autoMapper )
              {
                     //IRepository<Diploma> repoDiplomas = new Repository<Diploma>( context );

                     //var eduTypes = repoDiplomas.Context.EducationTypes.Select( x => x.Type ).ToList();

                     IRepository<Person> repo = new Repository<Person>( context );
                     var mapper = new MapEntity( autoMapper );
                     var personsFactory = new PersonsCollectionFactory( mapper, repo );

                     var firstPerson = repo.AllAsNoTracking().FirstOrDefault();
                     var mappedPerson = mapper.Map<Person, SearchPersonVM>( firstPerson );

                     var searchPersons = mapper
                            .ProjectTo<Person, SearchPersonVM>( repo.AllAsNoTracking() )
                            .OrderBy( x => x.FirstName )
                            .ThenBy( x => x.LastName ).ToList();

                     var personService = new PersonService( repo, mapper, personsFactory );

                     var result = personService.AllActive_SearchPersonVM().ToList();
                     //var personVM = new PersonVM
                     //{
                     //       Id = 7,
                     //       FirstName = "Costa",
                     //       LastName = "Costadinov",
                     //       CivilNumber = "8810251234",
                     //       GenderType = "man",
                     //};

                     //var person = mapper.Map<PersonVM, Person>( personVM );

                     //var genderCaseOne = repo.Context.Genders.Where( x => x.Type == personVM.GenderType ).FirstOrDefault();

                     //if ( genderCaseOne != null && genderCaseOne.Id > 0 )
                     //{
                     //       person.Gender = genderCaseOne;
                     //}

                     //repo.Update( person );

                     //repo.SaveChangesAsync().GetAwaiter().GetResult();

                     Console.WriteLine();


              }

              public static void AddressValidateTest( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<Address> repository = new Repository<Address>( context );
                     var mapper = new MapEntity( autoMapper );

                     //IQueryable<AddressVM>? defaultCollection =
                     //     mapper.ProjectTo<Address, AddressVM>( repository.AllAsNoTracking() );

                     var modelState = new ModelStateDictionary();
                     var ModelVm = new AddressVM
                     {
                            Country = "Bulgaria",
                            Region = "Tarnovo",
                            City = "Tarnovo",
                            Street = "Green Meadow",
                            Number = 8,
                            Floor = 4,
                            HasBeenDeleted = false,
                            AddressType = "permanent",
                            PersonId = 19
                     };

                     var validateService = new ValidateAddressVMService( repository );

                     validateService.Validate( modelState, ModelVm );

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
       }
}


/*

 //Cannot insert explicit value for identity column in table 'Persons' when IDENTITY_INSERT is set to OFF.

                     //repository.Context.Addresses.Entry( address ).State = EntityState.Added;

                     //person.PermanentAddress = address;

                     //repository.SaveChangesAsync().GetAwaiter().GetResult();


                     //if ( repo.Context.Genders.Any( x => x.Type == person.Gender.Type ) )
                     //{

                     //       //person.GenderId = 1;
                     //}
                     //else
                     //{
                     //       repo.Context.Genders.Entry( person.Gender ).State = EntityState.Added;
                     //}

                     //var genderState = repo.Context.Genders.Entry( person.Gender ).State;


//var person = repository.All()
                     //       .Where( x => x.Id == 19 )
                     //       .FirstOrDefault();

                     //var personState = repository.DbSet.Entry( person ).State;

                     //var addressVm = new AddressVM
                     //{
                     //       Country = "Bul",
                     //       Region = "Tarnovo",
                     //       City = "Tarrnovo",
                     //       Street = "Green Meadow",
                     //       Number = 8,
                     //       Floor = 4,
                     //       HasBeenDeleted = false,
                     //       AddressType = "permanent",
                     //       PersonId = 19
                     //};

                     //var address = mapper.Map<AddressVM, Address>( addressVm );

                     //repository.Context.Addresses.Entry( address ).State = EntityState.Added;

                     //person.PermanentAddress = address;

                     //repository.SaveChangesAsync().GetAwaiter().GetResult();

*/

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
*/