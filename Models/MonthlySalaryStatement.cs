using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class MonthlySalaryStatement
     {
          [Key]
          public int Id { get; set; }

          public int? EmployeeId { get; set; }
          public Employee? Employee { get; set; }

          public byte? Month { get; set; }

          public ushort? Year { get; set; }

          [StringLength(100,MinimumLength = 3)]
          public string? DepartmentName { get; set; }

          [StringLength(250,MinimumLength = 3)]
          public string? JobTitle { get; set; }

          public byte? WorkDays { get; set; }

          public byte? DaysWorked { get; set; }

          [StringLength(15)]
          public string? GeneralInternship { get; set; }

          [StringLength(15)]
          public string? ProfessionalInternship{ get; set; }

          /// <summary>
          /// Official Internship ; Service Internship
          /// </summary>
          [StringLength(15)]
          public string? PublicInternship{ get; set; }

          public int? PaymentTypeId { get; set; }
          public PaymentType? PaymentType { get; set; }


          [Column(TypeName = "decimal(18,2)")]
          public decimal? Salary { get; set; }


          [Column(TypeName = "decimal(18,2)")]
          public decimal? SocialSecurityIncome { get; set; }


          [Column(TypeName = "decimal(18,2)")]
          public decimal? TaxableAmount { get; set; }

          public ICollection<IncomePartStatement> IncomePartStatements { get; set; } = new HashSet<IncomePartStatement>();

          public ICollection<DeductionPartStatement> DeductionPartStatements { get; set; } = new HashSet<DeductionPartStatement>();

          public ICollection<RecapPartStatement> RecapPartStatements { get; set; } = new HashSet<RecapPartStatement>();
     }
}
