using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class Employee
     {
          [Key]
          public int Id { get; set; }

          public bool IsPresent { get; set; }

          [StringLength(50,MinimumLength = 3)]
          public string? NumberFromTheList { get; set; }

		[ForeignKey("Person")]
		public int? PersonId { get; set; }
		public Person? Person { get; set; }

          public int? EmpContractId { get; set; }
          public EmploymentContract? EmploymentContract { get; set; }


          [ForeignKey("Company")]
          public int? CompanyId { get; set; }
          public Company? Company { get; set; }

          public ICollection<TemporaryDisability> TemporaryDisabilities { get; set; } = new HashSet<TemporaryDisability>();

          public ICollection<Vacation> Vacations { get; set; } = new HashSet<Vacation>();

          public ICollection<MonthlySalaryStatement> MonthlySalaryStatements { get; set; } = new HashSet<MonthlySalaryStatement>();

          public ICollection<DeductionElement> DeductionElements { get; set; } = new HashSet<DeductionElement>();

          public ICollection<IncomeElement> IncomeElements { get; set; } = new HashSet<IncomeElement>();

     }
}