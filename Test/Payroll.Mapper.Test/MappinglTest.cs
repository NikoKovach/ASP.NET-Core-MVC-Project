using System.Reflection;
using AutoMapper;

using Payroll.Data;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.PersonViewModels;


namespace Test.Payroll
{
       [TestFixture]
       public class MappinglTest
       {
              private PayrollContext payrollContext;
              private IMapper? mapper;
              private IMapEntity? service;

              [SetUp]
              public void MappingSetUp()
              {
                     this.payrollContext = new PayrollContext();

                     var config = new AutoMapperBuilder().CreateMapperConfig();

                     this.mapper = config.CreateMapper();

                     this.service = new MapEntity( mapper );
              }

              [TearDown]
              public void MappingCleanup()
              {
                     this.payrollContext.Dispose();

                     this.mapper = default;

                     this.service = default;
              }

              [Test]
              public void MapPersonVMToPerson()
              {
                     PersonVM personDto = new PersonVM()
                     {
                            FirstName = "Dara",
                            MiddleName = "Smith",
                            LastName = "Bergendorf",
                            CivilNumber = "7703102225",
                            GenderType = "woman",
                            PhotoFilePath = "/css/someFile.css",
                            Id = 2
                     };

                     Person person = this.service.Map<PersonVM, Person>( personDto );

                     Assert.That( person, Is.InstanceOf<Person>() );

                     Assert.That( person.FirstName, Is.EqualTo( "Dara" ) );
              }

              [Test]
              public void MapPersonToPersonVM()
              {
                     Person person = new Person()
                     {
                            Id = 17,
                            FirstName = "Dara",
                            MiddleName = "Smith",
                            LastName = "Bergendorf",
                            EGN = "7703102225",
                            GenderId = 7,
                            PhotoFilePath = "/css/someFile.css"
                     };

                     PersonVM personViewModel = this.service.Map<Person, PersonVM>( person );

                     Assert.That( personViewModel, Is.InstanceOf<PersonVM>() );

                     Assert.That( personViewModel.CivilNumber, Is.EqualTo( person.EGN ) );
              }

              [Test]
              public void MapEmployeeVMToEmployee()
              {
                     EmployeeVM empDto = new EmployeeVM()
                     {
                            PersonId = 17,
                            CompanyId = 6,
                            NumberFromTheList = "555",
                            IsPresent = true
                     };

                     Employee employee = this.service.Map<EmployeeVM, Employee>
                                                 ( empDto );

                     Assert.That( employee, Is.InstanceOf<Employee>() );

                     Assert.Multiple( () =>
                     {
                            Assert.That( employee.CompanyId, Is.EqualTo( empDto.CompanyId ) );

                            Assert.That( employee.NumberFromTheList,
                                   Is.EqualTo( empDto.NumberFromTheList ) );
                     } );
              }

              [Test]
              public void MapEmployeeToEmployeeVM()
              {
                     Employee employee = new Employee()
                     {
                            Id = 2,
                            PersonId = 17,
                            CompanyId = 6,
                            NumberFromTheList = "555",
                            IsPresent = true
                     };

                     var employeeDto = this.service.Map<Employee, EmployeeVM>
                                                 ( employee );

                     Assert.That( employeeDto, Is.InstanceOf<EmployeeVM>() );

                     Assert.Multiple( () =>
                     {
                            Assert.That( employee.CompanyId, Is.EqualTo( employeeDto.CompanyId ) );

                            Assert.That( employee.NumberFromTheList,
                                   Is.EqualTo( employeeDto.NumberFromTheList ) );
                     } );
              }


              [TestCaseSource( nameof( GetPersonsLists ) )]
              public void MapCollectionOfPersonToCollectionOfPersonVM( List<Person> persons )
              {
                     List<PersonVM> personDtoList = this.service
                            .Map<List<Person>, List<PersonVM>>( persons );

                     Assert.That( personDtoList, Is.InstanceOf<List<PersonVM>>() );

                     Assert.That( personDtoList[ 0 ].CivilNumber, Is.EqualTo( persons[ 0 ].EGN ) );
              }

              //######################################################################

              [TestCaseSource( nameof( IsInstanceOf_MethodParameters ) )]
              public void Result_IsInstanceOf_Source( Type sourceType, Type destinationType, object source )
              {
                     MethodInfo? baseMethod = this.service.GetType()
                            .GetMethod( nameof( this.service.Map ), BindingFlags.Instance | BindingFlags.Public );

                     MethodInfo genericMethod = baseMethod.MakeGenericMethod( sourceType, destinationType );

                     object? result = genericMethod.Invoke( this.service, new object[] { source } );

                     Assert.That( result, Is.InstanceOf( destinationType ) );
              }

              [TestCaseSource( nameof( IsInstanceOf_MethodParameters ) )]
              public void ResultId_IsEqualTo_SourceId( Type sourceType, Type destinationType, object source )
              {
                     MethodInfo? baseMethod = this.service.GetType()
                            .GetMethod( nameof( this.service.Map ), BindingFlags.Instance | BindingFlags.Public );

                     MethodInfo genericMethod = baseMethod.MakeGenericMethod( sourceType, destinationType );

                     object? result = genericMethod.Invoke( this.service, new object[] { source } );
                     //var result = this.service.Map<TSource, TDestination>( source );

                     PropertyInfo? resultPropId = result.GetType().GetProperty( "Id" ); // AddressId
                     int? resultId = (int?) resultPropId.GetValue( result );

                     PropertyInfo? sourcePropId = source.GetType().GetProperty( "Id" );
                     int? sourceId = (int?) sourcePropId.GetValue( source );

                     if ( resultId != null && sourceId != null )
                     {
                            Assert.That( resultId, Is.EqualTo( sourceId ) );
                     }
              }

              //######################################################################

              private static object[] IsInstanceOf_MethodParameters()
              {
                     object[] methodParameters = new object[ DestinationTypesList().Count ];

                     if ( DestinationTypesList().Count == SourceTypesList().Count )
                     {
                            for ( int i = 0; i < DestinationTypesList().Count; i++ )
                            {
                                   methodParameters[ i ] = new object[] { SourceTypesList()[ i ], DestinationTypesList()[ i ], SourceList()[ i ] };
                            }
                     }

                     return methodParameters;
              }

              private static IEnumerable<List<Person>> GetPersonsLists()
              {
                     List<Person> persons = new List<Person>()
                     {
                            new Person
                            {
                                   Id                   = 17,
                                   FirstName            = "Dara",
                                   MiddleName    = "Smith",
                                   LastName             = "Bergendorf",
                                   EGN                  = "7703102225",
                                   GenderId             = 7,
                                   PhotoFilePath = "/css/someFile.css"
                            },
                            new Person
                            {
                                   Id                   = 18,
                                   FirstName            = "Todor",
                                   MiddleName    = "T.",
                                   LastName             = "Todorov",
                                   EGN                  = "7777777777",
                                   GenderId             = 8,
                                   PhotoFilePath = "/css/someFile.css"
                            },
                            new Person
                            {
                                   Id                   = 19,
                                   FirstName            = "Denko",
                                   MiddleName    = "S.",
                                   LastName             = "Denkov",
                                   EGN                  = "8888888888",
                                   GenderId             = 8,
                                   PhotoFilePath = "/css/someFile.css"
                            },
                     };

                     return [ persons ];
              }

              private static List<Type> SourceTypesList()
              {
                     return new List<Type>
                     {
                            typeof(DiplomaVM ) ,
                            typeof(Diploma ),
                            typeof(ContactInfoVM),
                            typeof(ContactInfo),
                            typeof(AddressVM),
                            typeof(Address),
                            typeof(IdDocumentVM),
                            typeof(IdDocument),
                     };
              }

              private static List<Type> DestinationTypesList()
              {
                     return new List<Type>
                     {
                            typeof(Diploma) ,
                            typeof(DiplomaVM),
                            typeof(ContactInfo),
                            typeof(ContactInfoVM),
                            typeof(Address),
                            typeof(AddressVM),
                            typeof(IdDocument),
                            typeof(IdDocumentVM),
                     };
              }

              private static List<Object> SourceList()
              {
                     List<Object> sources = new List<object>
                     {
                            new DiplomaVM
                            {
                                   Id = 10,
                                   DiplomaRegNumber = "R202410",
                                   DateOfIssue = DateTime.Parse( "01.10.2024" ),
                                   Seria = "S_Omega",
                                   SerialNumber = "242424",
                                   EducationTypeName = "High school diploma",
                            },
                            new Diploma
                            {
                                   Id = 555,
                                   DiplomaRegNumber = "R55555",
                                   DateOfIssue = DateTime.Parse( "01.10.2055" ),
                                   Seria = "S_555",
                                   SerialNumber = "555",
                                   EducationId = 55
                            },
                            new ContactInfoVM
                            {
                                   Id = 4,
                                   PhoneNumberOne = "7777777777777",
                                   PhoneNumberTwo = "8888888888888",
                                   E_MailAddress1 = "zzzzzz.DDDD@xxxxxx-yyyy.zzz",
                                   E_MailAddress2 = "qqqqqqq@wwwwwwwwww",
                                   WebSite = "domain",
                                   HasBeenDeleted = false,
                            },
                            new ContactInfo
                            {
                                   Id = 1234,
                                   PhoneNumberOne = "1234",
                                   PhoneNumberTwo = "4321",
                                   E_MailAddress1 = "4321@4321.zzz",
                                   E_MailAddress2 = "1234@1234",
                                   WebSite = "domain",
                                   HasBeenDeleted = false,
                            },
                            new AddressVM
                            {
                                   Id = 8888,
                                   AddressType ="current",
                                   Country = "Spain",
                                   Region ="YYY",
                                   City="City of Spain",
                                   Street="Any street",
                                   Number = 11,
                            },
                            new Address
                            {
                                   Id = 8888,
                                   Country = "Canada",
                                   Region ="Region X",
                                   City="City Y",
                                   Street="Any street",
                                   Number = 8888,
                                   Floor = 8,
                                   ApartmentNumber = 8
                            },
                            new IdDocumentVM
                            {
                                   Id = 7777777,
                                   DocumentName = "person card",
                                   Nationality = "XXX",
                                   DateOfExpire = DateTime.Parse("30.12.2311"),
                                   ColorOfEyes = "green",
                                   IssuingAuthority = "Ministry of the Interior",
                                   DateOfIssue = DateTime.Parse("29.12.2300"),
                                   IsValid = true,
                            },
                            new IdDocument
                            {
                                   Id = 7777777,
                                   DocumentType = new DocumentType{Id = 2,DocumentName = "Drive License" },
                                   //DocumentName = "drive license",
                                   Nationality = "YYYY",
                                   DateOfExpire = DateTime.Parse("30.12.2222"),
                                   ColorOfEyes = "green",
                                   IssuingAuthority = "Ministry of the Interior",
                                   DateOfIssue = DateTime.Parse("29.12.2201"),
                                   IsValid = false,
                            },
                     };

                     return sources;
              }
       }
}

