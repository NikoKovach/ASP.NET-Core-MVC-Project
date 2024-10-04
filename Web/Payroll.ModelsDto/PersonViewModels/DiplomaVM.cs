using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class DiplomaVM
       {
              public int Id { get; set; }

              [Display( Name = "Person Id" )]
              public int? PersonId { get; set; }

              [Display( Name = "Education Id" )]
              public int? EducationId { get; set; }

              [Display( Name = "Reg Number" )]
              [Required]
              [StringLength( 20, MinimumLength = 3 )]
              public string DiplomaRegNumber { get; set; }

              [Display( Name = "Registration Date" )]
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
              public bool HasBeenDeleted { get; set; }
       }
}

//public int EducationId { get; set; }