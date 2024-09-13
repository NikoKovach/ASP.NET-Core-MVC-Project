using Payroll.ViewModels.ModelRestrictions;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class RestrictionsFactory : IViewModelLimitationsFactory
       {
              private Dictionary<string, ViewModelRestrictions> limitations;

              public RestrictionsFactory()
              {
                     this.limitations = new Dictionary<string, ViewModelRestrictions>();

                     SetEmployeeVMRestrictions();
              }

              public Dictionary<string, ViewModelRestrictions> Limitations => this.limitations;

              private void SetEmployeeVMRestrictions()
              {
                     EmployeeVMLimitations empVMRestrictions = new EmployeeVMLimitations
                     {
                            //[FileValidationFilter( [ ".jpg", ".png" ], 1024 * 1024 )]
                            AllowedExtensions = [ ".jpg", ".png", ".bmp", ".gif", ".tiff", ".tga" ],
                            MaxImageSizeInBytes = 1024 * 1024,
                            MinImageSizeInBytes = 1024
                     };

                     this.limitations[ "EmployeeVM" ] = empVMRestrictions;
              }
       }
}
