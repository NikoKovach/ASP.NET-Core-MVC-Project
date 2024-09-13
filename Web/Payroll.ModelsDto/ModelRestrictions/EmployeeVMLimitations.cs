namespace Payroll.ViewModels.ModelRestrictions
{
       public class EmployeeVMLimitations : ViewModelRestrictions
       {
              public string[] AllowedExtensions { get; set; }

              public int MaxImageSizeInBytes { get; set; }

              public int MinImageSizeInBytes { get; set; }
       }
}
