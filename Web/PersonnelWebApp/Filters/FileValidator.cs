namespace PersonnelWebApp.Filters
{
       public static class FileValidator
       {
              public static bool IsFileExtensionAllowed( IFormFile file, string[] allowedExtensions )
              {
                     var extension = Path.GetExtension( file.FileName ).ToLower();

                     return allowedExtensions.Contains( extension );
              }

              public static bool IsFileSizeWithinLimit( IFormFile file, long maxSizeInBytes )
              {
                     return file.Length <= maxSizeInBytes;
              }
       }
}
