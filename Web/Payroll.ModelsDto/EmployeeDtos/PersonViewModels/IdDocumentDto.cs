using Payroll.Models.EnumTables;

namespace Payroll.ModelsDto.EmployeeDtos.PersonDtos
{
     public class IdDocumentDto
     {
          public int Id { get; set; }

          public int? PersonId { get; set; }

          public string DocumentNumber { get; set; }

          public int? DocumentTypeId { get; set; }

          public string? DocumentName { get; set; }

          public string? Nationality { get; set; }

          public DateTime? DateOfBirth { get; set; }

          public DateTime DateOfExpire { get; set; }

          public string? PlaceOfBirth { get; set; }

          public int? Height { get; set; }

          public string? ColorOfEyes { get; set; }

          public string IssuingAuthority { get; set; }

          public DateTime DateOfIssue { get; set; }

          public string? VehicleCategory { get; set; }

          public DateTime? DateOfAcquisitionOfTheCategory { get; set; }

          public bool IsValid { get; set; }
     }
}