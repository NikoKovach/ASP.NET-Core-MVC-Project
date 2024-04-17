using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class RecapPartStatement
     {
          [Key]
          public int Id { get; set; }

          [StringLength(200,MinimumLength =3)]
          public string Name { get; set; }

          [Column(TypeName = "decimal(20,2)")]
          public decimal? Amount { get; set; }

          public int? SalaryStatementId { get; set; }
          public MonthlySalaryStatement? MonthlySalaryStatement { get; set; }

          public int? EmployeeId { get; set; }
     }
}
