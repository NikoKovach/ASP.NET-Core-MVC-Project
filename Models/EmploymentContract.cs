using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Payroll.Models.EnumTables;

namespace Payroll.Models
{
    public class EmploymentContract
     {
          [Key]
          public int Id { get; set; }

          public bool IsActive { get; set; }

          [Required]
          [StringLength(20,MinimumLength = 1)]
          public string ContractNumber { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime ContractDate { get; set; }


          [ForeignKey("ContractType")]
          public int ContractTypeId { get; set; }
          public ContractType ContractType { get; set; }


          [StringLength(20,MinimumLength = 1)]
          public string? WorkExperience { get; set; }

          [StringLength(20,MinimumLength = 1)]
          public string? SpecialtyWorkExperience  { get; set; }


          [ForeignKey("LaborCodeArticle")]
          public int LaborCodeArticleId { get; set; }
          public LaborCodeArticle? LaborCodeArticle { get; set; }

          public int? TrialPeriod { get; set; }

          [StringLength(50,MinimumLength = 3)]
          public string? IsNegotiatedInFavorOf { get; set; }

          [Required]
          [StringLength(250,MinimumLength = 3)]
          public string JobTitle { get; set; }

          [ForeignKey("Department")]
          public int? DeparmentId { get; set; }
          public Department? Department { get; set; }

          /// <summary>
          /// EconomicActivityCode
          /// </summary>
          [StringLength(250,MinimumLength = 3)]
          public string? EAC { get; set; }

          /// <summary>
          /// National Classification of Occupations and Positions
          /// </summary>
          [StringLength(250,MinimumLength = 3)]
          public string? NCOP { get; set; }

          /// <summary>
          /// in hours
          /// </summary>
          [Required]
          [Column("DayWorkTime")]
          public byte WorkTime { get; set; }

          [Required]
          [Column(TypeName = "decimal(18,2)")]
          public decimal Salary { get; set; }

          /// <summary>
          /// Percent for specialty work experience
          /// </summary>
          [Required]
          public double PercentSWE { get; set; }

          public byte? SalaryDayOfTheMonth { get; set; }

          [Required]
          [Column("PaidAnnualLeaveInDays")]
          public byte PaidLeaveInDays { get; set; }

          public byte AdditionalPaidAnnualLeaveInDays { get; set; }

          public byte ProbationInMonths { get; set; }

          [Required]
          public byte NoticePeriodInDays { get; set; }

          public bool ReceivedAJobDescription { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime StartingWorkDate { get; set; }

          /// <summary>
          /// Date of receipt of contract
          /// </summary>
          [Column(TypeName = "date")]
          public DateTime? DateOfReceipt { get; set; }

          public int EmployeeId { get; set; }
          public Employee Employee { get; set; }

          /// <summary>
          /// Place of conclusion of the employment contract
          /// </summary>
          public int? PlaceId { get; set; }
          public PlaceOfRegistration? PlaceOfRegistration { get; set; }

          /// <summary>
          /// Employee work place
          /// </summary>
          public int? WorkPlaceId { get; set; }
          public PlaceOfRegistration? WorkPlace { get; set; }

          public ICollection<Annex> SupplementaryAgreements { get; set; } = new HashSet<Annex>();

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
     }
}
