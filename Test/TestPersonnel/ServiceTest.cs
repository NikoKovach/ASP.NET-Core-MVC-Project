using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
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

              private static string GeneratError()
              {
                     string newRow = "\r\n";
                     StringBuilder sb = new StringBuilder();

                     sb.Append( $"Image cannot be loaded. Available decoders:{newRow}" );
                     sb.Append( $"- BMP : BmpDecoder{newRow}" );
                     sb.Append( $"- PBM : PbmDecoder{newRow}" );
                     sb.Append( $"- PNG : PngDecoder{newRow}" );
                     sb.Append( $"- QOI : QoiDecoder{newRow}" );
                     sb.Append( $"- Webp : WebpDecoder{newRow}" );
                     sb.Append( $"- BMP : BmpDecoder{newRow}" );
                     sb.Append( $"- BMP : BmpDecoder{newRow}" );
                     sb.Append( $"- BMP : BmpDecoder{newRow}" );


                     return sb.ToString();
              }

              public static void PersonsServiceTest( PayrollContext context, IMapper autoMapper )
              {
                     IRepository<Person> repository = new Repository<Person>( context );
                     var mapEntity = new MapEntity( autoMapper );

                     //PersonFilterVM? filterVM = new()
                     //{
                     //       //Id = 6,
                     //       SearchName = "pet",
                     //       CivilID = "4646"
                     //};

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

                     var result = mapEntity.Map<List<PersonVM>, List<Person>>( entitiesForEdit );


                     Console.WriteLine( result.Count );
                     //Console.WriteLine( nameof( filterVM.CivilID ) );

                     //string? sort = "LastName_desc";

                     //var personsFactory = new PersonsCollectionFactory( mapEntity, repository.AllAsNoTracking() );

                     //personsFactory.Filtrate( filterVM, sort );
              }
       }
}

/*

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