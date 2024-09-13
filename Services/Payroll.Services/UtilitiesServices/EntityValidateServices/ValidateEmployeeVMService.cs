using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.Data.Common;
using Payroll.Models;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.ModelRestrictions;
using SixLabors.ImageSharp;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateEmployeeVMService : IValidate
       {
              private const string newRow = "\r\n";

              private IQueryable<Employee> employees;
              private EmployeeVMLimitations limitations;
              private List<string> fieldErrors;

              public ValidateEmployeeVMService( IRepository<Employee> employees,
                     IViewModelLimitationsFactory factory )
              {
                     this.employees = employees.AllAsNoTracking();

                     limitations = (EmployeeVMLimitations) factory.Limitations[ nameof( EmployeeVM ) ];

                     this.fieldErrors = new List<string>();
              }

              public void Validate<EmployeeVM>( ModelStateDictionary modelState, EmployeeVM viewModel )
              {
                     ValidateNumberFromTheList( modelState, viewModel );

                     ValidateEmployeePicture( modelState, viewModel, this.limitations );
              }

              private void ValidateNumberFromTheList<EmployeeVM>( ModelStateDictionary modelState,
                                                                                                                                     EmployeeVM viewModel )
              {
                     this.fieldErrors.Clear();

                     PropertyInfo? viewProperty = viewModel.GetType().GetProperty( "NumberFromTheList" );

                     string propName = viewProperty.Name;

                     object? propertyValue = viewProperty.GetValue( viewModel );

                     if ( propertyValue is null )
                     {
                            return;
                     }

                     string? propValue = propertyValue.ToString();

                     if ( string.IsNullOrEmpty( propValue ) )
                            return;

                     bool isNumber = int.TryParse( propValue, out int numberFromTheList );

                     if ( !isNumber )
                     {
                            string error = string.Format( OutputMessages.ErrorNotANumber );
                            fieldErrors.Add( error );

                            AddModelStateError( modelState, propName );

                            return;
                     }

                     PropertyInfo? propEmployeeId = viewModel.GetType().GetProperty( "Id" );
                     int? employeeIdVal = (int?) propEmployeeId.GetValue( viewModel );

                     bool listNumberWasChanged = ListNumberIsChanged( numberFromTheList, employeeIdVal );

                     if ( !listNumberWasChanged )
                            return;

                     LstNumberIsZeroOrNegative( numberFromTheList, fieldErrors );

                     PropertyInfo? propCompanyId = viewModel.GetType().GetProperty( "CompanyId" );
                     int? companyIdVal = (int?) propCompanyId.GetValue( viewModel );

                     ListNumberExists( numberFromTheList, companyIdVal, fieldErrors );

                     if ( this.fieldErrors.Count > 0 )
                     {
                            AddModelStateError( modelState, propName );
                     }
              }

              private void ValidateEmployeePicture<EmployeeVM>( ModelStateDictionary modelState,
                     EmployeeVM viewModel, EmployeeVMLimitations? limitations )
              {
                     this.fieldErrors.Clear();

                     PropertyInfo? imageProperty = viewModel.GetType().GetProperty( "ProfileImage" );
                     string imagePropName = imageProperty.Name;

                     IFormFile? image = (IFormFile?) imageProperty.GetValue( viewModel );

                     if ( image != null )
                     {
                            UseImageSharp( image );

                            if ( image is not IFormFile file || image.Length == 0 )
                            {
                                   this.fieldErrors.Add( OutputMessages.ErrorInvalidFile );
                            }

                            if ( !FileValidator.IsFileSizeWithinLimit( image, limitations.MaxImageSizeInBytes,
                                   limitations.MinImageSizeInBytes ) )
                            {
                                   double maxMbSize = (double) limitations.MaxImageSizeInBytes / 1024 / 1024;
                                   double minKbSize = (double) limitations.MaxImageSizeInBytes / 1024;

                                   string errorMessage = string.Format( OutputMessages.ErrorFileSize, minKbSize, maxMbSize );

                                   this.fieldErrors.Add( errorMessage );
                            }

                            if ( this.fieldErrors.Count > 0 )
                            {
                                   AddModelStateError( modelState, imagePropName );
                            }
                     }
              }

              //****************************************************************
              private void UseImageSharp( IFormFile image )
              {
                     using var stream = image.OpenReadStream();

                     try
                     {
                            ImageInfo? imageInfo = Image.Identify( stream );
                     }
                     catch ( UnknownImageFormatException )
                     {
                            string? allowedExtensions = String.Join( ", ", limitations.AllowedExtensions )
                                                                                                .Replace( ".", "" )
                                                                                                .ToUpper();

                            string errorMessage = string.Format( OutputMessages.ErrorFileFormat, allowedExtensions );
                            fieldErrors.Add( errorMessage );
                     }
                     catch ( InvalidImageContentException )
                     {
                            string invalidContentError = OutputMessages.ErrorFileContent;
                            fieldErrors.Add( invalidContentError );
                     }
              }

              private void AddModelStateError( ModelStateDictionary modelState, string propName )
              {
                     string generalError = GenerateErrorString( this.fieldErrors );

                     modelState.AddModelError( propName, generalError );
              }

              private string GenerateErrorString( List<string> fieldErrors )
              {
                     StringBuilder sb = new StringBuilder();

                     for ( int i = 0; i < fieldErrors.Count; i++ )
                     {
                            if ( i == fieldErrors.Count - 1 )
                            {
                                   sb.Append( $"{i + 1}.{fieldErrors[ i ]}" );
                                   break;
                            }

                            sb.Append( $"{i + 1}.{fieldErrors[ i ]}{newRow}" );
                     }

                     return sb.ToString();
              }

              private bool ListNumberIsChanged( int numberFromTheList, int? employeeIdVal )
              {
                     if ( employeeIdVal == null || employeeIdVal < 1 )
                            return false;

                     var oldValue = employees.Where( x => x.Id == employeeIdVal )
                                                                   .Select( x => int.Parse( x.NumberFromTheList ) )
                                                                   .FirstOrDefault();

                     bool result = ( numberFromTheList == oldValue ) ? false : true;

                     return result;
              }

              private void LstNumberIsZeroOrNegative( int numberFromTheList, List<string> fieldErrors )
              {
                     if ( numberFromTheList < 1 )
                     {
                            string errorIsNegative = string.Format( OutputMessages.ErrorNumberIsNegative );
                            fieldErrors.Add( errorIsNegative );
                     }
              }

              private void ListNumberExists( int numberFromTheList, int? companyIdVal, List<string> fieldErrors )
              {
                     if ( companyIdVal != null && companyIdVal > 0 )
                     {
                            List<int>? empPropertyList = employees
                                                                        .Where( x => x.CompanyId == companyIdVal )
                                                                        .Select( x => int.Parse( x.NumberFromTheList ) )
                                                                        .ToList();

                            if ( empPropertyList.Contains( numberFromTheList ) )
                            {
                                   string errorIsExists = string.Format( OutputMessages.ErrorValueExists, numberFromTheList );
                                   fieldErrors.Add( errorIsExists );
                            }
                     }
              }
       }
}

/*

Image cannot be loaded. Available decoders:
 - BMP : BmpDecoder
 - PBM : PbmDecoder
 - PNG : PngDecoder
 - QOI : QoiDecoder
 - Webp : WebpDecoder
 - GIF : GifDecoder
 - JPEG : JpegDecoder
 - TGA : TgaDecoder
 - TIFF : TiffDecoder


 using var stream = image.OpenReadStream();
                     try
                     {
                            var imageInfo = Image.Identify( stream );

                            //if ( imageInfo != null )
                            //{
                            //       var formatName = imageInfo.Metadata.DecodedImageFormat.Name;
                            //       var width = imageInfo.Width;
                            //       var height = imageInfo.Height;
                            //}
                     }
                     catch ( InvalidImageContentException exc )
                     {
                            //Invalid content ?
                            var error = exc.Message;

                     }
                     catch ( UnknownImageFormatException formatExc )
                     {
                            var error = formatExc.Message;

                            Console.WriteLine( error );
                     }

//if ( !FileValidator.IsFileExtensionAllowed( image, limitations.AllowedExtensions ) )
                            //{
                            //       string? allowedExtensions = String.Join( ", ", limitations.AllowedExtensions )
                            //                                                                    .Replace( ".", "" )
                            //                                                                    .ToUpper();

                            //       string errorMessage = string.Format( OutputMessages.ErrorFileFormat, allowedExtensions );
                            //       fieldErrors.Add( errorMessage );
                            //}
*/