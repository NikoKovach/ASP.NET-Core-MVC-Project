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

              public static string CreateRelativePath( string fullPath, string relativeFolderName, string appFolderName )
              {
                     if ( string.IsNullOrEmpty( fullPath ) )
                     {
                            throw new FileNotFoundException();
                     }

                     //relativeFolderName, appFolderName
                     int startIndex = fullPath.IndexOf( appFolderName ) + appFolderName.Length;

                     string subRelativeFileFolder = fullPath.Substring( startIndex ).Replace( @"\", "/" );

                     string relativePath = $"{relativeFolderName}{subRelativeFileFolder}";

                     return relativePath;
              }

              /// <summary>
              /// -- Directory.Exists( Path.Combine( appFolder, subFolderName ) ) --
              /// 'rootFolderPath' other name 'appFolderPath'
              /// </summary>
              /// <param name="rootFolderPath" >appFolderPath</param>
              /// <param name="subFolderName"></param>
              /// <returns>true - id directory exists ; otherwise - false</returns>
              public static bool DirExists( string rootFolderPath, string subFolderName )
              {
                     if ( !Directory.Exists( Path.Combine( rootFolderPath, subFolderName ) ) )
                            return false;

                     return true;
              }

              public static string? GetPath( params string[]? paths )
              {
                     if ( paths == null )
                            return default;

                     if ( paths.Length < 1 )
                            return default;

                     return Path.Combine( paths );
              }

              public static string? CreateDir( params string[]? paths )
              {
                     if ( paths == null )
                            return default;

                     if ( paths.Length < 1 )
                            return default;

                     DirectoryInfo? newDir = Directory.CreateDirectory( GetPath( paths ) );

                     return newDir.FullName;
              }
       }
}

//EnvironmentService.GetPath( appFolder, companyFolderName );
//appFolder = AppFolder
//relativeFolder = /app-folder
//fullPath
// ......\AppFolder\Булгарстрой-World-АД\Employees\Dida-B-Koleva\Dida-B-Koleva-profile-img.jpg

//relativePath
/// /app-folder/Булгарстрой-World-АД/Employees/Asena-Y-Koleva/koleva-clock.png

