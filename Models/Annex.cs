using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Payroll.Models.EnumTables;

namespace Payroll.Models
{
     public class Annex
     {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(20,MinimumLength = 1)]
          public string AgreementNumber { get; set; }

          /// <summary>
          /// Date of agreement or date of change
          /// </summary>
          [Required]
          [Column(TypeName = "date")]
          public DateTime DateOfAgreementOrChange { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime CountedFromDate { get; set; }

          public int? LaborCodeArticleId { get; set; }

          public int? DepartmentId { get; set; }
          public Department? Department { get; set; }

          [StringLength(200,MinimumLength = 3)]
          public string? JobTitle { get; set; }

          /// <summary>
          /// Economic Activity Code
          /// </summary>
          [StringLength(250,MinimumLength = 3)]
          public string? EAC { get; set; }

          /// <summary>
          /// National Classification of Occupations and Positions
          /// </summary>
          [StringLength(250,MinimumLength = 3)]
          public string? NCOP { get; set; }

          public int? WorkPlaceId { get; set; }
       
          public byte? DayWorkTime { get; set; }

          [Column(TypeName = "decimal(18,2)")]
          public decimal? Salary { get; set; }

          /// <summary>
          /// Percent for specialty work experience
          /// </summary>
          public double? PercentSWE { get; set; }

          public byte? SalaryDayOfTheMonth { get; set; }

          public byte? PaidAnnualLeaveInDays { get; set; }

          public byte? AdditionalPaidAnnualLeaveInDays { get; set; }

          public byte? NoticePeriodInDays { get; set; }

          public bool? ReceivedAJobDescription { get; set; }

          /// <summary>
          /// Date of receipt of annex
          /// </summary>
          [Column(TypeName = "date")]
          public DateTime? DateOfReceipt { get; set; }

          public int? EmpContractId { get; set; }
          public EmploymentContract? EmpContract { get; set; }

          public bool HasBeenDeleted { get; set; }
     }
}
