using LegalFramework.Services.DocumentGenerator;

namespace TestPersonnel.Demo
{
       public static class DocGeneratorDemo
       {
              public static void FirstPdf()
              {
                     var tempDoc = new TempDocument();
                     string path = @"D:\SoftUni Courses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Web\PersonnelWebApp\AppFolder\Temp";

                     var filePath = tempDoc.CreateFile( path, "laborContract" );

                     //PdfGenerator.Create();

                     Console.WriteLine();
              }
       }
}
