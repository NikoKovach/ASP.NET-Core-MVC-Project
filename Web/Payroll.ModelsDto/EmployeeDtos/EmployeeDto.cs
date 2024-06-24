namespace Payroll.ModelsDto.EmployeeDtos
{
     public class EmployeeDto
     {
          public int Id { get; set; }

          public int? PersonId { get; set; }

          public int? CompanyId { get; set; }

		public string? NumberFromTheList { get; set; }

		public bool IsPresent{ get; set; }
     }
}

          //public PersonDto? Person { get; set; }

          //public int? EmpContractId { get; set; }