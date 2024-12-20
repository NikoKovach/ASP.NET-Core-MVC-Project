﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.Data.Common;
using Payroll.Models;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.ModelRestrictions;
using SixLabors.ImageSharp;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateEmployeeVMService : ValidateBaseClass, IValidate<ValidateBaseModel>
       {
              private IQueryable<Employee> employees;
              private EmployeeVMLimitations limitations;

              public ValidateEmployeeVMService( IRepository<Employee> employees,
                     IViewModelLimitationsFactory factory )
              {
                     this.employees = employees.AllAsNoTracking();

                     limitations = (EmployeeVMLimitations) factory.Limitations[ nameof( EmployeeVM ) ];
              }

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                 [CallerMemberName] string actionName = "", params object[] parameters )
              {
                     base.ModelState = modelState;

                     EmployeeVM model = (EmployeeVM) viewModel;

                     ValidateNumberFromTheList( model );

                     ValidateEmployeePicture( model, this.limitations );

                     PropertyInfo? personIdProperty = viewModel
                                                                                  .GetType()
                                                                                  .GetProperty( nameof( model.PersonId ) );

                     PropertyInfo? companyIdProperty = viewModel
                                                                                      .GetType()
                                                                                      .GetProperty( nameof( model.CompanyId ) );

                     RenameInvalidValueErrorMsg( personIdProperty, nameof( model.PersonId ) );

                     RenameInvalidValueErrorMsg( companyIdProperty, nameof( model.CompanyId ) );
              }

              public void RenameInvalidValueErrorMsg( PropertyInfo? property, string propertyName )
              {
                     ModelStateEntry? entryToChange = this.ModelState[ propertyName ];

                     if ( entryToChange == null )
                            return;

                     string? entryValue = entryToChange.RawValue.ToString();

                     if ( entryToChange.ValidationState == ModelValidationState.Invalid
                            && string.IsNullOrEmpty( entryValue ) )
                     {
                            string errorMsg = "The value '' is invalid.";

                            ModelError? errorEmptyString = entryToChange
                                                                                       .Errors
                                                                                       .FirstOrDefault( x => x.ErrorMessage.Equals( errorMsg ) );

                            bool isRemoved = entryToChange.Errors.Remove( errorEmptyString );
                            if ( isRemoved )
                            {
                                   string? displayName = this.GetDisplayName( property );

                                   string newErrorMsg = $"{displayName} : {this.newRow}{errorMsg}";

                                   entryToChange.Errors.Add( newErrorMsg );
                            }
                     }
              }

              private void ValidateNumberFromTheList( EmployeeVM viewModel )
              {
                     this.FieldErrors.Clear();

                     if ( string.IsNullOrEmpty( viewModel.NumberFromTheList ) )
                            return;

                     string? propertyValue = viewModel.NumberFromTheList;

                     bool isNumber = int.TryParse( propertyValue, out int numberFromTheList );

                     PropertyInfo? property = viewModel
                                                                  .GetType()
                                                                  .GetProperty( nameof( viewModel.NumberFromTheList ) );

                     if ( !isNumber )
                     {
                            string error = string.Format( OutputMessages.ErrorNotANumber );
                            this.FieldErrors.Add( error );

                            string displayName = this.GetDisplayName( property );

                            AddModelStateError( displayName, property.Name );

                            return;
                     }

                     int? employeeIdVal = viewModel.Id;

                     if ( employeeIdVal != null && employeeIdVal > 0 )
                     {
                            bool listNumberWasChanged = ListNumberIsChanged( numberFromTheList, employeeIdVal );

                            if ( !listNumberWasChanged )
                                   return;
                     }

                     ListNumberIsZeroOrNegative( numberFromTheList );

                     ListNumberExists( numberFromTheList, viewModel.CompanyId );

                     if ( this.FieldErrors.Count > 0 )
                     {
                            string displayName = this.GetDisplayName( property );

                            this.AddModelStateError( displayName, property.Name );
                            //this.AddModelStateError( displayName );
                     }
              }

              private void ValidateEmployeePicture( EmployeeVM viewModel, EmployeeVMLimitations? limitations )
              {
                     this.FieldErrors.Clear();

                     IFormFile? image = viewModel.ProfileImage;

                     if ( image != null )
                     {
                            UseImageSharp( image );

                            if ( image is not IFormFile file || image.Length == 0 )
                            {
                                   this.FieldErrors.Add( OutputMessages.ErrorInvalidFile );
                            }

                            bool fileSizeIsInLimit = image.Length >= limitations.MinImageSizeInBytes
                                                              && image.Length <= limitations.MaxImageSizeInBytes;

                            if ( !fileSizeIsInLimit )
                            {
                                   double maxMbSize = (double) limitations.MaxImageSizeInBytes / 1024 / 1024;
                                   double minKbSize = (double) limitations.MaxImageSizeInBytes / 1024;

                                   string errorMessage = string.Format( OutputMessages.ErrorFileSize, minKbSize, maxMbSize );

                                   this.FieldErrors.Add( errorMessage );
                            }

                            if ( this.FieldErrors.Count > 0 )
                            {
                                   PropertyInfo? imageProperty = viewModel
                                                                          .GetType()
                                                                          .GetProperty( nameof( viewModel.ProfileImage ) );

                                   string displayName = this.GetDisplayName( imageProperty );

                                   this.AddModelStateError( displayName, imageProperty.Name );
                                   //AddModelStateError( displayName );
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
                            this.FieldErrors.Add( errorMessage );
                     }
                     catch ( InvalidImageContentException )
                     {
                            string invalidContentError = OutputMessages.ErrorFileContent;
                            this.FieldErrors.Add( invalidContentError );
                     }
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

              private void ListNumberIsZeroOrNegative( int numberFromTheList )
              {
                     if ( numberFromTheList < 1 )
                     {
                            string errorIsNegative = string.Format( OutputMessages.ErrorNumberIsNegative );
                            this.FieldErrors.Add( errorIsNegative );
                     }
              }

              private void ListNumberExists( int numberFromTheList, int? companyIdVal )
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
                                   this.FieldErrors.Add( errorIsExists );
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
*/