using System.ComponentModel.DataAnnotations;
using Payroll.ViewModels.CustomValidation;

namespace Payroll.ViewModels.PersonViewModels
{
       public class DiplomaVM : ValidateBaseModel
       {
              public int? Id { get; set; }

              [Display( Name = "Reg Number" )]
              [Required]
              [StringLength( 20, MinimumLength = 3 )]
              public string? DiplomaRegNumber { get; set; }

              [Display( Name = "Reg. Date" )]
              [DataType( DataType.Date )]
              [DateIsEarlier( ErrorMessage = "Date cannot be greater than today's date !" )]
              public DateTime? DateOfIssue { get; set; }

              [StringLength( 20, MinimumLength = 3 )]
              public string? Seria { get; set; }

              [Display( Name = "Serial Number" )]
              [StringLength( 20, MinimumLength = 3 )]
              public string? SerialNumber { get; set; }

              [Display( Name = "Education" )]
              [Required]
              [StringLength( 20, MinimumLength = 3 )]
              public string? EducationTypeName { get; set; }

              [StringLength( 250, MinimumLength = 3 )]
              public string? Speciality { get; set; }

              [StringLength( 250, MinimumLength = 3 )]
              public string? Profession { get; set; }

              [Display( Name = "Deleted" )]
              public bool? HasBeenDeleted { get; set; }

              [Display( Name = "Person Id" )]
              [Required]
              public int? PersonId { get; set; }
       }
}
