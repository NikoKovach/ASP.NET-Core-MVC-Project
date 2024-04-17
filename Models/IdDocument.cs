using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Payroll.Models.EnumTables;

namespace Payroll.Models
{
    public class IdDocument
     {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(40,MinimumLength = 2)]
          public string DocumentNumber { get; set; }

          [ForeignKey("DocumentType")]
          public int? DocumentTypeId { get; set; }
          public DocumentType? DocumentType { get; set; }

          [StringLength(200,MinimumLength = 2)]
          public string? Nationality { get; set; }

          [Column(TypeName = "date")]
          public DateTime? DateOfBirth { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime DateOfExpire { get; set; }

          [StringLength(200,MinimumLength = 2)]
          public string? PlaceOfBirth { get; set; }

          public int? Height { get; set; }

          [StringLength(50,MinimumLength = 2)]
          public string? ColorOfEyes { get; set; }

          [Required]
          [StringLength(200,MinimumLength = 2)]
          public string IssuingAuthority { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime DateOfIssue { get; set; }

          [StringLength(25,MinimumLength = 1)]
          public string? VehicleCategory { get; set; }

          [Column(TypeName = "date")]
          public DateTime? DateOfAcquisitionOfTheCategory { get; set; }

          public bool IsValid { get; set; }


          [ForeignKey("Person")]
          public int? PersonId { get; set; }
          public Person? Person { get; set; }

          public bool HasBeenDeleted { get; set; }

     }
}
