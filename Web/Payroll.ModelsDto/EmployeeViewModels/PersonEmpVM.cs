using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class PersonEmpVM
       {
              [Display( Name = "Person Id" )]
              public int PersonId { get; set; }

              [Display( Name = "Full Name" )]
              public string? FullName { get; set; }

              public int? GenderId { get; set; }

              [Display( Name = "Gender" )]
              public string? GenderType { get; set; }

              public string EGN { get; set; }

              [Display( Name = "Photo" )]
              public string? PhotoFilePath { get; set; }

              [Display( Name = "Permanent address" )]
              public string? PermanentAddress { get; set; }

              [Display( Name = "Current address" )]
              public string? CurrentAddress { get; set; }
       }
}
