using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

namespace Payroll.ModelsDto.EmployeeDtos
{
     public class EmployeeDto
     {
          public int Id { get; set; }

          public bool IsActual { get; set; }

          public string? NumberFromTheList { get; set; }

          public int? PersonId { get; set; }
          public PersonDto? Person { get; set; }

          //public int? EmpContractId { get; set; }

          public int? CompanyId { get; set; }
     }
}
