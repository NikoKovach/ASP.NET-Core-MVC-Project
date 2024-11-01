using System.Text.RegularExpressions;

namespace Payroll.Services.Utilities
{
       public static class EnvironmentService
       {
              public static string ModifyName( string name )
              {
                     if ( string.IsNullOrEmpty( name ) )
                     {
                            return default;
                     }

                     string pattern = @"[\w+]+";

                     MatchCollection? regexResult = Regex.Matches( name, pattern );

                     string? result = string.Join( '-', regexResult );

                     return result;
              }

              public static string CreateRelativePath( string fullPath, string relativeFolder, string appFolder )
              {
                     int startIndex = fullPath.IndexOf( appFolder ) + appFolder.Length;

                     string subRelativeImageFolder = fullPath.Substring( startIndex ).Replace( @"\", "/" );

                     string relativePath = $"{relativeFolder}{subRelativeImageFolder}";

                     return relativePath;
              }
       }
}


//appFolder = AppFolder
//relativeFolder = /app-folder
//fullPath
// ......\AppFolder\Булгарстрой-World-АД\Employees\Dida-B-Koleva\Dida-B-Koleva-profile-img.jpg

//relativePath
/// /app-folder/Булгарстрой-World-АД/Employees/Asena-Y-Koleva/koleva-clock.png

