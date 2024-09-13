using Microsoft.AspNetCore.Http;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public static class FileValidator
       {
              public static bool IsFileExtensionAllowed( IFormFile file, string[] allowedExtensions )
              {
                     var extension = Path.GetExtension( file.FileName ).ToLower();

                     return allowedExtensions.Contains( extension );
              }

              public static bool IsFileSizeWithinLimit( IFormFile file, long maxSizeInBytes, int minSizeInBytes )
              {
                     return ( file.Length >= minSizeInBytes && file.Length <= maxSizeInBytes );
              }
       }
}
