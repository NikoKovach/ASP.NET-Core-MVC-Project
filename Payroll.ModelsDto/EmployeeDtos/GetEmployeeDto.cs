namespace Payroll.ModelsDto.EmployeeDtos
{
     public class GetEmployeeDto
     {
          public int EmployeeId { get; set; }

          public int PersonId { get; set; }

          public bool IsActual { get; set; }

          public string FirstName { get; set; }

          public string? MiddleName { get; set; }

          public string LastName { get; set; }

          public string EGN { get; set; }

          public string? GenderType { get; set; }

          public string? PermanentAddress { get; set; }

          public string? CurrentAddress { get; set; }

          public string? PhotoFilePath { get; set; }

          public string? PhoneNumberOne { get; set; }

          public string? PhoneNumberTwo { get; set; }

          public string? E_MailAddress1 { get; set; }

          public string? WebSite { get; set; }

          public string? DocumentName { get; set; }

          public string? DocumentNumber { get; set; }      

          public string? NumberFromTheList { get; set; }

          public string? JobTitle { get; set; }

          public string? DepartmentName { get; set; }

          public string? WorkExperience { get; set; }

          public string? SpecialtyWorkExperience  { get; set; }

          public bool HasBeenDeleted { get; set; }
     }
}
