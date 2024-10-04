using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
       public class IdDocumentVM : IdDocumentEmpVM
       {
              public int Id { get; set; }

              [Display( Name = "Person Id" )]
              public int? PersonId { get; set; }

              [StringLength( 200, MinimumLength = 2 )]
              public string? Nationality { get; set; }

              [Display( Name = "Date of birth" )]
              [Column( TypeName = "date" )]
              public DateTime? DateOfBirth { get; set; }

              [Display( Name = "Date of expire" )]
              [Required]
              [Column( TypeName = "date" )]
              public DateTime DateOfExpire { get; set; }

              [Display( Name = "Place of birth" )]
              [StringLength( 200, MinimumLength = 2 )]
              public string? PlaceOfBirth { get; set; }

              public int? Height { get; set; }

              [Display( Name = "Color of eyes" )]
              [StringLength( 50, MinimumLength = 2 )]
              public string? ColorOfEyes { get; set; }

              [Display( Name = "Issuing authority" )]
              [Required]
              [StringLength( 200, MinimumLength = 2 )]
              public string IssuingAuthority { get; set; }

              [Display( Name = "Date of issue" )]
              [Required]
              [Column( TypeName = "date" )]
              public DateTime DateOfIssue { get; set; }

              [Display( Name = "Vehicle category" )]
              [StringLength( 25, MinimumLength = 1 )]
              public string? VehicleCategory { get; set; }

              [Display( Name = "Category - date of acquisition" )]
              [Column( TypeName = "date" )]
              public DateTime? DateOfAcquisitionOfTheCategory { get; set; }

              public bool IsValid { get; set; }
       }
}