using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.ModelsDto;
using Payroll.ModelsDto.EmployeeDtos;

public class Program
{
	private static void Main( string[] args )
	{
		var context = new PayrollContext();

		var config = new AutoMapperBuilder().CreateMapperConfig();
		var mapper = config.CreateMapper();

		//**************************************************************
		//var modelDto = GetDto();

		//var entity = GetEntity( context, mapper );

		//context.Entry( entity ).State = EntityState.Deleted;

		//var entityDto = GetMapperDto( entity, mapper );
		//companyDto.UniqueIdentifier = "0000000000";

		//IAddUpdateEntity service = new AddUpdateEntity(context,mapper);

		//        await service.AddEntityAsync<Person,PersonDto>( modelDto as PersonDto );

		var someCollection = TestProjectTo<Company,CompanyDto>(context,mapper);

		var company = someCollection
			.Where( x => x.Id == 6 )
			.Count();

		//RunEmployeeService( context );

		Console.WriteLine();
	}

	private static GetEmployeeDto GetMapperDto( Employee entity, IMapper mapper )
	{
		var dtoEntity = mapper.Map<GetEmployeeDto>( entity );
		//var dtoList = mapper.Map<List<Employee>, List<GetEmployeeDto>>(entity);

		return dtoEntity;
	}

	private static async Task<IList<GetEmployeeDto>> GetEntity( PayrollContext context, IMapper mapper )
	{
		var dtoList = await new GetEmployeeMapping().MapAllEmployeesQueryable( context, 6 ).ToListAsync();

		return dtoList;
	}

	private static void RunEmployeeService( PayrollContext context )
	{
		//var customMapper = new GetEmployeeMapping();

		//var service = new EmployeeService(context,customMapper);

		//var result = service.GetAllEmployeesAsync( 6 ).GetAwaiter().GetResult();

		//Console.WriteLine();
	}

	private static IQueryable<TResult> TestProjectTo<TSource,TResult>
		(PayrollContext db,IMapper mapperConfig) 
		where TSource : class
	{

		var query = db.Set<TSource>().ProjectTo<TResult>(mapperConfig.ConfigurationProvider);

		return query;
	}

}