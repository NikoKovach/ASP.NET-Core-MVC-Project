using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

		EnvironmentDemo();

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
		DbSet<Employee> dbSet = context.Employees;
		var dtoList = await new GetEmployeeMapping().MapAllEmployeesQueryable( dbSet, 6 ).ToListAsync();

		return dtoList;
	}

	private static void EnvironmentDemo(  )
	{
		////"   Геострой   Холдинг   " АД    
		//string text = "\"   Геострой-   Холдинг   Инвест    '     ЕАД       \"";
		//string textTwo = "\"   Expressions developer using    555'    Ltd.       \"";


		//string pattern1 = @"[\S+]+";//@"\w";
		//var reg1 = Regex.Matches( text,pattern1 );
		
		//string pattern2 = @"[\w+]+";
		//var reg2 = Regex.Matches( textTwo,pattern2 );

		//var result = string.Join( '-', reg2 );


		//var text3 = text.Split( ' ',
		//				StringSplitOptions.RemoveEmptyEntries );
		

		string sourceDirectory = @"C:\zzz-source";
		string destinationDirectory = @"C:\zzz-destination";


		//string[] fileNames = Directory.GetFiles(destinationDirectory);

		try
		{
		    Directory.Move(sourceDirectory, destinationDirectory);
		}
		catch (Exception e)
		{
		    Console.WriteLine(e.Message);
		}

		string[] newfileNames = Directory.GetFiles(destinationDirectory);
		var newDir = Directory.GetDirectoryRoot(destinationDirectory);

	}

	private static IEnumerable<string> TrimText( IEnumerable<string> textArr )
	{
		return textArr.ToList()
					.Select( x => x.Trim() )
					.ToList();
	}

	private static IQueryable<TResult> TestProjectTo<TSource,TResult>
		(PayrollContext db,IMapper mapperConfig) 
		where TSource : class
	{

		var query = db.Set<TSource>().ProjectTo<TResult>(mapperConfig.ConfigurationProvider);

		return query;
	}

}

/*
 var testPath = Path.GetFullPath( Assembly.GetExecutingAssembly().Location );

		var appFolder = Path.GetFullPath( testPath + @"\..\..\..\..\");
		Console.WriteLine(appFolder);

		string companyName = "\"Company 1\" АД";

		int index = companyName.IndexOf('"');

		while ( index != -1 )
		{
			//if ( index == -1 ) break;

			companyName = companyName.Remove(index,1);
			index = companyName.IndexOf('"');
		}
		
		string newCompanyName = companyName
			.Trim()
			.Replace( ' ', '-' )
			.ToUpper();

		Console.WriteLine(newCompanyName);

		if ( !Directory.Exists(Path.Combine(appFolder,newCompanyName)) )
		{
			string path = appFolder + newCompanyName;
			Directory.CreateDirectory(path);
		}
 */