using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Payroll.ViewModels.EmployeeViewModels;

namespace PersonnelWebApp.Filters
{
       public class FileValidationFilter( string[] allowedExtensions, long maxSize ) : ActionFilterAttribute
       {
              public override void OnActionExecuting( ActionExecutingContext context )
              {
                     var paramFile = context.ActionArguments
                                                 .FirstOrDefault( x => x.Key.Equals( "empViewModel" ) )
                                                 .Value as EmployeeVM;

                     IFormFile? image = paramFile.ProfileImage;

                     List<string> fieldErrors = new List<string>();

                     if ( image != null )
                     {
                            if ( image is not IFormFile file || image.Length == 0 )
                            {
                                   fieldErrors.Add( "File is invalid !" );
                            }

                            if ( !FileValidator.IsFileExtensionAllowed( image, allowedExtensions ) )
                            {
                                   var allowedExtensionsMessage = String.Join( ", ", allowedExtensions )
                                                                                                         .Replace( ".", "" )
                                                                                                         .ToUpper();

                                   string errorMessage = $"Invalid file type.Please upload {allowedExtensionsMessage} file.";

                                   fieldErrors.Add( errorMessage );
                            }

                            if ( !FileValidator.IsFileSizeWithinLimit( image, maxSize ) )
                            {
                                   var mbSize = (double) maxSize / 1024 / 1024;

                                   string errorMessage = $"File size exceeds the maximum allowed size  ({mbSize} MB).";

                                   fieldErrors.Add( errorMessage );
                            }

                            StringBuilder sb = new StringBuilder();

                            for ( int i = 0; i < fieldErrors.Count; i++ )
                            {
                                   if ( i == fieldErrors.Count - 1 )
                                   {
                                          sb.Append( $"{i + 1}.{fieldErrors[ i ]}" );
                                          break;
                                   }

                                   sb.Append( $"{i + 1}.{fieldErrors[ i ]}\r\n" );
                            }

                            context.ModelState.AddModelError( nameof( paramFile.ProfileImage ), sb.ToString() );
                     }
              }
       }
}
