using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class IdDocumentVM : ValidateBaseModel
       {
              public int? Id { get; set; }

              [Required]
              [Display( Name = "Document Type" )]
              public string? DocumentName { get; set; }

              [Required]
              [StringLength( 40, MinimumLength = 2 )]
              [Display( Name = "Document Number" )]
              public string? DocumentNumber { get; set; }

              [StringLength( 200, MinimumLength = 2 )]
              public string? Nationality { get; set; }

              [DataType( DataType.Date )]
              [Display( Name = "Date Of Birth" )]
              public DateTime? DateOfBirth { get; set; }

              [Required]
              [DataType( DataType.Date )]
              [Display( Name = "Date Of Expire" )]
              public DateTime? DateOfExpire { get; set; }

              [StringLength( 200, MinimumLength = 2 )]
              [Display( Name = "Place Of Birth" )]
              public string? PlaceOfBirth { get; set; }

              public int? Height { get; set; }

              [StringLength( 50, MinimumLength = 2 )]
              [Display( Name = "Color Of Eyes" )]
              public string? ColorOfEyes { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 2 )]
              [Display( Name = "Issuing Authority" )]
              public string? IssuingAuthority { get; set; }

              [Required]
              [DataType( DataType.Date )]
              [Display( Name = "Date Of Issue" )]
              public DateTime? DateOfIssue { get; set; }

              [StringLength( 25, MinimumLength = 1 )]
              [Display( Name = "Vehicle Category" )]
              public string? VehicleCategory { get; set; }

              [DataType( DataType.Date )]
              [Display( Name = "Category - Date of acquisition" )]
              public DateTime? DateOfAcquisitionOfTheCategory { get; set; }

              [Display( Name = "Is Valid" )]
              public bool IsValid { get; set; }

              [Display( Name = "Has Been Deleted" )]
              public bool HasBeenDeleted { get; set; }

              public DateTime? DeletionDate { get; set; }

              [Display( Name = "Person Id" )]
              public int? PersonId { get; set; }
       }
}