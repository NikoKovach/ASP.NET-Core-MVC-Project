using AutoMapper;

namespace WebConsoleAppPersonnel
{
	internal class SecondProgram
	{
		public static void SecondMain()
		{
			Address empAddres = new Address()
			{
			    City = "Mumbai",
			    State = "Maharashtra",
			    Country = "India"
			};

			Employee emp = new Employee
			{
			    Name = "James",
			    Salary = 20000,
			    Department = "IT",
			    Address = empAddres
			};

            //Initialize the AutoMapper
			var mapper = InitializeAutomapper();

			  //Way1: Mapping emp Object with EmployeeDTO Object
			var empDTO = mapper.Map<EmployeeDto>(emp);

			  //Way2: Mapping emp Object with EmployeeDTO Object
			  //var empDTO = mapper.Map<Employee, EmployeeDTO>(emp);

			Console.WriteLine("Name:" + empDTO.Name + ", Salary:" + empDTO.Salary + 
				", Department:" + empDTO.Department);

			Console.WriteLine("City:" + empDTO.AddressDTO.City + ", State:" + 
			empDTO.AddressDTO.State + ", Country:" + empDTO.AddressDTO.Country);

		}

		public static Mapper InitializeAutomapper()
		{
			//Provide all the Mapping Configuration
			var config = new MapperConfiguration(cfg => {
				cfg.CreateMap<Address,AddressDto>();
			      //Configuring Employee and EmployeeDTO
				cfg.CreateMap<Employee,EmployeeDto>()
				   .ForMember(dest => dest.AddressDTO, act => act.MapFrom(src =>		src.Address));
			});

			//Create an Instance of Mapper and return that Instance
			var mapper = new Mapper(config);

			return mapper;
		}
	}
}
