using Newtonsoft.Json;
using Payroll.Data;
using Payroll.Mapper.AutoMapper;
using TestPersonnel.Demo;

public class Program
{
       private static void Main( string[] args )
       {
              var context = new PayrollContext();
              var config = new AutoMapperBuilder().CreateMapperConfig();
              var mapper = config.CreateMapper();

              //**************************************************************
              ServiceTest.AutoMapperTest( context, mapper );

              //ServiceTest.TestCustomMapper( context );
              //Map.UseDelegate( context );
              //ServiceTest.TestValidate( context );
              //EnvironmentDemo();



              //var someCollection = TestProjectTo<Company,CompanyViewModel>(context,mapper);

              //var company = someCollection
              //	.Where( x => x.Id == 6 )
              //	.Count();

              //JsonDemo();

              Console.WriteLine();
       }

       private static void JsonDemo()
       {
              var stringList = new List<string>() { "One", "Two", "Five" };
              var querybleColl = stringList.AsQueryable();

              var pagingList = new PaginatedList<string>( stringList, 3, 1 );

              var pagingDict = PaginatedCollection<string>.CreateCollectionAsync( querybleColl, 2 );

              var jsonPagingList =
                     JsonConvert.SerializeObject( pagingDict, Formatting.Indented );

              Console.WriteLine( jsonPagingList );

       }

       //private static GetEmployeeDto GetMapperDto( Employee entity, IMapper mapper )
       //{
       //	var dtoEntity = mapper.Map<GetEmployeeDto>( entity );
       //	//var dtoList = mapper.Map<List<Employee>, List<GetEmployeeDto>>(entity);

       //	return dtoEntity;
       //}

       //private static async Task<IList<GetEmployeeDto>> GetEntity( PayrollContext context, IMapper mapper )
       //{
       //	DbSet<Employee> dbSet = context.Employees;
       //	var dtoList = await new GetEmployeeMapping().MapAllEmployeesQueryable( dbSet, 6 ).ToListAsync();

       //	return dtoList;
       //}

       //private static void EnvironmentDemo(  )
       //{
       //	////"   Геострой   Холдинг   " АД    
       //	//string text = "\"   Геострой-   Холдинг   Инвест    '     ЕАД       \"";
       //	//string textTwo = "\"   Expressions developer using    555'    Ltd.       \"";


       //	//string pattern1 = @"[\S+]+";//@"\w";
       //	//var reg1 = Regex.Matches( text,pattern1 );

       //	//string pattern2 = @"[\w+]+";
       //	//var reg2 = Regex.Matches( textTwo,pattern2 );

       //	//var result = string.Join( '-', reg2 );


       //	//var text3 = text.Split( ' ',
       //	//				StringSplitOptions.RemoveEmptyEntries );


       //	string sourceDirectory = @"C:\zzz-source";
       //	string destinationDirectory = @"C:\zzz-destination";

       //	try
       //	{
       //	    Directory.Move(sourceDirectory, destinationDirectory);
       //	}
       //	catch (Exception e)
       //	{
       //	    Console.WriteLine(e.Message);
       //	}

       //	string[] newfileNames = Directory.GetFiles(destinationDirectory);
       //	var newDir = Directory.GetDirectoryRoot(destinationDirectory);

       //}

       //private static IEnumerable<string> TrimText( IEnumerable<string> textArr )
       //{
       //	return textArr.ToList()
       //				.Select( x => x.Trim() )
       //				.ToList();
       //}

       //private static IQueryable<TResult> TestProjectTo<TSource,TResult>
       //	(PayrollContext db,IMapper mapperConfig) 
       //	where TSource : class
       //{

       //	var query = db.Set<TSource>().ProjectTo<TResult>(mapperConfig.ConfigurationProvider);

       //	return query;
       //}

}
