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
                     //appFolder = AppFolder
                     //relativeFolder = /app-folder
                     //fullPath
                     // ......\AppFolder\Булгарстрой-World-АД\Employees\Dida-B-Koleva\Dida-B-Koleva-profile-img.jpg

                     //relativePath
                     /// /app-folder/Булгарстрой-World-АД/Employees/Asena-Y-Koleva/koleva-clock.png

                     int startIndex = fullPath.IndexOf( appFolder ) + appFolder.Length;

                     string subRelativeImageFolder = fullPath.Substring( startIndex ).Replace( @"\", "/" );

                     string relativePath = $"{relativeFolder}{subRelativeImageFolder}";

                     return relativePath;
              }
       }
}

/*
  //public static void CreateFolder( string rootPath, string folderName )
              //{
              //       string modifiedFolderName = ModifyName( folderName );

              //       if ( DirectoryExists( rootPath, modifiedFolderName ) )
              //       {
              //              return;
              //       }

              //       Directory.CreateDirectory( NewFolderPath( rootPath, modifiedFolderName ) );
              //}


              //public static bool DirectoryExists( string rootFolder, string folderName )
              //{
              //       string newFolderPath = Path.Combine( rootFolder, folderName );

              //       if ( !Directory.Exists( newFolderPath ) )
              //              return false;

              //       return true;
              //}

              //public static string NewFolderPath( string rootPath, string folderName )
              //{
              //       string newFolderPath = Path.Combine( rootPath, folderName );

              //       return newFolderPath;
              //}
*/

