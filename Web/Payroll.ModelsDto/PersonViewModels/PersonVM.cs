using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class PersonVM : ValidateBaseModel
       {
              [Display( Name = "Id" )]
              public int? Id { get; set; }

              [Display( Name = "First Name" )]
              [StringLength( 100, MinimumLength = 3 )]
              public string? FirstName { get; set; }

              [Display( Name = "Middle Name" )]
              [StringLength( 100, MinimumLength = 3 )]
              public string? MiddleName { get; set; }

              [Display( Name = "Last Name" )]
              [StringLength( 100, MinimumLength = 3 )]
              public string? LastName { get; set; }

              [Display( Name = "Gender" )]
              [StringLength( 10, MinimumLength = 3 )]
              public string? GenderType { get; set; }

              [Display( Name = "Civil Number" )]
              [StringLength( 10, MinimumLength = 10 )]
              public string? CivilNumber { get; set; }

              [Display( Name = "Profile Photo" )]
              public string? PhotoFilePath { get; set; }
       }
}

//public  Address? PermanentAddress { get; set; }
//public  Address? CurrentAddress { get; set; }
//string Gender.Type { get; set; }
//public int? EmployeeId { get; set; }