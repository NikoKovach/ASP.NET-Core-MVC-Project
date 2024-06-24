using AutoMapper;
using Payroll.Data;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.ModelsDto.PersonViewModels;
using Payroll.Services.Services;


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

			this.service = new MapEntity(payrollContext,mapper);
		}

		[TearDown]
		public void MappingCleanup() 
		{
			this.payrollContext.Dispose();

			this.mapper = default;

			this.service = default;
		}

		[Test]
		public void MapPersonDtoToPerson()
		{
			PersonDto personDto = new PersonDto() 
			{ 
				FirstName		= "Dara",
				MiddleName	= "Smith",
				LastName		= "Bergendorf",
				EGN			= "7703102225",
				GenderType	= "woman",
				PhotoFilePath	= "/css/someFile.css"
			};

			Person person = this.service.Map<PersonDto,Person>( personDto );

			Assert.That( person, Is.InstanceOf<Person>() );

			Assert.That( person.FirstName, Is.EqualTo( "Dara" ) );

		}

		[Test]
		public void MapPersonToPersonDto()
		{
			Person person = new Person() 
			{ 
				Id			= 17,
				FirstName		= "Dara",
				MiddleName	= "Smith",
				LastName		= "Bergendorf",
				EGN			= "7703102225",
				GenderId		= 7,
				PhotoFilePath	= "/css/someFile.css"
			};

			PersonDto personDto = this.service.Map<Person,PersonDto>( person );

			Assert.That( personDto, Is.InstanceOf<PersonDto>() );

			Assert.That( personDto.EGN, Is.EqualTo( person.EGN ) );
		}

		[Test]
		public void MapEmployeeDtoToEmployee()
		{
			EmployeeDto empDto = new EmployeeDto() 
			{ 
				
				PersonId			= 17,
				CompanyId			= 6,
				NumberFromTheList	= "555",
				IsPresent			= true
			};

			Employee employee = this.service.Map< EmployeeDto,Employee>
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
		public void MapEmployeeToEmployeeDto()
		{
			Employee employee = new Employee() 
			{ 
				Id				= 2,
				PersonId			= 17,
				CompanyId			= 6,
				NumberFromTheList	= "555",
				IsPresent			= true
			};

			var employeeDto = this.service.Map<Employee,EmployeeDto>
							( employee );

			Assert.That( employeeDto, Is.InstanceOf<EmployeeDto>() );

			Assert.Multiple( () =>
			{
				Assert.That( employee.CompanyId, Is.EqualTo( employeeDto.CompanyId ) );

				Assert.That( employee.NumberFromTheList,
					Is.EqualTo( employeeDto.NumberFromTheList ) );
			} );
		}


		[TestCaseSource(nameof(GetPersonsLists))]  
		public void MapCollectionOfPersonToCollectionOfPersonDto(List<Person> persons)
		{
			List<PersonDto> personDtoList = this.service
				.Map<List<Person>,List<PersonDto>>( persons );

			Assert.That( personDtoList, Is.InstanceOf<List<PersonDto>>() );

			Assert.That( personDtoList[0].EGN, Is.EqualTo( persons[0].EGN ) );
		}

		private static IEnumerable<List<Person>> GetPersonsLists()
		{
			List<Person> persons = new List<Person>() 
			{ 
				new Person
				{
					Id			= 17,
					FirstName		= "Dara",
					MiddleName	= "Smith",
					LastName		= "Bergendorf",
					EGN			= "7703102225",
					GenderId		= 7,
					PhotoFilePath	= "/css/someFile.css"
				},
				new Person
				{
					Id			= 18,
					FirstName		= "Todor",
					MiddleName	= "T.",
					LastName		= "Todorov",
					EGN			= "7777777777",
					GenderId		= 8,
					PhotoFilePath	= "/css/someFile.css"
				},
				new Person
				{
					Id			= 19,
					FirstName		= "Denko",
					MiddleName	= "S.",
					LastName		= "Denkov",
					EGN			= "8888888888",
					GenderId		= 8,
					PhotoFilePath	= "/css/someFile.css"
				},
			};

			return [persons];
		}
	}
}

//Gender
//Id	Type
//5	man
//6	woman