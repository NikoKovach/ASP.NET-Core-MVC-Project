namespace Payroll.Models
{
     public class PaymentType
     {
          public int Id { get; set; }

          public int Name { get; set; }

          public ICollection<MonthlySalaryStatement> MonthlySalaryStatements { get; set; } = new HashSet<MonthlySalaryStatement>();
     }
}