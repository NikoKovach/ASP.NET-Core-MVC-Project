using AutoMapper;
using Payroll.Data;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;
using Payroll.Services.Services;
using Payroll.Services.Services.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Payroll
{
	[TestFixture]
	public class MappinglTest
	{
		private PayrollContext payrollContext;
		private IMapper? mapper;
		private AddUpdateEntity? service;

		[SetUp]
		public void MappingSetUp() 
		{ 
			this.payrollContext = new PayrollContext();

			var config = new AutoMapperBuilder().CreateMapperConfig();

			this.mapper = config.CreateMapper();

			this.service = new AddUpdateEntity(payrollContext,this.mapper);
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

			Person person = this.service.CreateObject<Person, PersonDto>( personDto );

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

			PersonDto personDto = this.service.CreateObject<PersonDto,Person>( person );

			Assert.That( personDto, Is.InstanceOf<PersonDto>() );

			Assert.That( personDto.EGN, Is.EqualTo( person.EGN ) );

		}

		[Test]
		public void MapEmployeeDtoToEmployee()
		{
			CreateEmployeeDto empDto = new CreateEmployeeDto() 
			{ 
				
				PersonId			= 17,
				CompanyId			= 6,
				NumberFromTheList	= "555",
				IsPresent			= true
			};

			Employee employee = this.service
				.CreateObject<Employee, CreateEmployeeDto>( empDto );

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

			var employeeDto = this.service
				.CreateObject<CreateEmployeeDto, Employee>( employee );

			Assert.That( employeeDto, Is.InstanceOf<CreateEmployeeDto>() );

			Assert.Multiple( () =>
			{
				Assert.That( employee.CompanyId, Is.EqualTo( employeeDto.CompanyId ) );

				Assert.That( employee.NumberFromTheList,
					Is.EqualTo( employeeDto.NumberFromTheList ) );
			} );
		}
	}
}

//Gender
//Id	Type
//5	man
//6	woman