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
              //ServiceTest.AutoMapperTest( context, mapper );
              //ServiceTest.TestCustomMapper( context );
              //ServiceTest.TestValidate( context, mapper );

              //ServiceTest.AddressValidateTest( context, mapper );

              //ServiceTest.ConfigurationTest();
              ServiceTest.PersonPartTest( context, mapper );

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

}
