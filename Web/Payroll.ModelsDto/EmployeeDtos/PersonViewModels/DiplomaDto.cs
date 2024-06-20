using Payroll.Models.EnumTables;

namespace Payroll.ModelsDto.EmployeeDtos.PersonDtos
{
     public class DiplomaDto
     {
          public int Id { get; set; }

          public string DiplomaRegNumber { get; set; }

          public DateTime? DateOfIssue { get; set; }

          public string? Seria { get; set; }

          public string? SerialNumber { get; set; }

          public string? EducationTypeName { get; set; }

          public int EducationId { get; set; }
          
          //public EducationType? EducationType { get; set; }

          public string? Speciality { get; set; }

          public string? Profession { get; set; }

          public int PersonId { get; set; }

          public bool HasBeenDeleted { get; set; }
     }
}
