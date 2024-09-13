using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.Services;
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
                     int personId = 6;
                     var employee = repository.All()
                                                                   .Where( x => x.Person.Id == personId ).Include( i => i.Person )
                                                                   .FirstOrDefault();

                     context.Entry( employee ).State = EntityState.Detached;

                     string revativePath2 = @"/app-folder/Булгарстрой-World-АД/Employees/Gocho-B-Petrov/man.png";
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
                     IRepository<Employee> repository = new Repository<Employee>( context );
                     var mapEntity = new MapEntity( autoMapper );

                     var restrictionsFactory = new RestrictionsFactory();
                     var customProjections = new CustomProjections();

                     var validateService = new ValidateEmployeeVMService( repository, restrictionsFactory );
                     var empService = new EmployeeService( repository, mapEntity, customProjections );
                     //***************************************************************

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
                     */

                     //************************************************************

                     //var file = @"D:\NK_Pictures\Arrioch-Elements-Leaf.ico";
                     //var file = @"D:\NK_Pictures\emp-3.jpg"; //
                     var file = @"D:\SoftUni Courses\A Exercises\AA Git Projects\Images\DSC_5605.jpg";

                     using var stream = new MemoryStream( File.ReadAllBytes( file ).ToArray() );
                     var formFile = new FormFile( stream, 0, stream.Length, "streamFile", file.Split( @"\" ).Last() );

                     EmployeeVM viewModel = new EmployeeVM
                     {
                            Id = 25,
                            PersonId = 6,
                            CompanyId = 8,
                            NumberFromTheList = "5", // "2"
                            IsPresent = true,
                            ProfileImage = formFile // formFile
                     };

                     var modelState = new ModelStateDictionary();
                     validateService.Validate<EmployeeVM>( modelState, viewModel );

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
       }
}

/*

Image cannot be loaded. Available decoders:
 - BMP : BmpDecoder
 - PBM : PbmDecoder
 - PNG : PngDecoder
 - QOI : QoiDecoder
 - Webp : WebpDecoder
 - GIF : GifDecoder
 - JPEG : JpegDecoder
 - TGA : TgaDecoder
 - TIFF : TiffDecoder



                     //var finalDate = date2.ToString( "yyyy-MM-dd" ); //"MMMM dd, yyyy"
                     //var date2 = DateTime.UtcNow.ToString( "yyyy-MM-dd_HHmmss" );
                     //var date = $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month}-{DateTime.UtcNow.Day}";


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
*/
