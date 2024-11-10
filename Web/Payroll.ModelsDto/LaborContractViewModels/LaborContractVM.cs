using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.LaborContractViewModels
{
       public class LaborContractVM : ValidateBaseModel
       {
              [Display( Name = "Contract Id" )]
              public int? Id { get; set; }

              [Required]
              [Display( Name = "Is Active" )]
              public bool? IsActive { get; set; }

              [Required]
              [Display( Name = "Contract type" )]
              public string? ContractType { get; set; }

              [Required]
              [StringLength( 20, MinimumLength = 1 )]
              [Display( Name = "Contract number" )]
              public string? ContractNumber { get; set; }

              [Required]
              [DataType( DataType.Date )]
              [Display( Name = "Contract date" )]
              public DateTime? ContractDate { get; set; }

              [StringLength( 20, MinimumLength = 2 )]
              [Display( Name = "Work Experience" )]
              public string? WorkExperience { get; set; }

              [StringLength( 20, MinimumLength = 2 )]
              [Display( Name = "Specialty Work Experience" )]
              public string? SpecialtyWorkExperience { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 1 )]
              [Display( Name = "Labor Code Article" )]
              public string? LaborCodeArticle { get; set; }

              [Display( Name = "Trial Period" )]
              public int? TrialPeriod { get; set; }

              [StringLength( 50, MinimumLength = 3 )]
              [Display( Name = "In Favor Of" )]
              public string? IsNegotiatedInFavorOf { get; set; }

              [Required]
              [StringLength( 250, MinimumLength = 3 )]
              [Display( Name = "Job title" )]
              public string? JobTitle { get; set; }

              [Required]
              [StringLength( 100, MinimumLength = 3 )]
              [Display( Name = "Department" )]
              public string? DepartmentName { get; set; }

              /// <summary>
              /// Economic Activity Code
              /// </summary>
              [StringLength( 250, MinimumLength = 3 )]
              [Display( Name = "EAC" )]
              public string? EAC { get; set; }

              /// <summary>
              /// National Classification of Occupations and Positions
              /// </summary>
              [StringLength( 250, MinimumLength = 3 )]
              [Display( Name = "NCOP" )]
              public string? NCOP { get; set; }

              [Required]
              [Display( Name = "Day Work Time" )]
              public byte? WorkTime { get; set; }

              [Required]
              [Range( typeof( decimal ), "50.00", "10 000 000.00" )]
              //[Column( TypeName = "decimal(18,2)" )]
              [Display( Name = "Labor Reward" )]
              public decimal? Salary { get; set; }

              /// <summary>
              /// Percent for specialty work experience
              /// </summary>
              [Required]
              [Display( Name = "% Work Experience" )]
              public double? PercentSWE { get; set; }

              [Display( Name = "Pay Date of Month" )]
              public byte? SalaryDayOfTheMonth { get; set; }

              [Required]
              [Display( Name = "Paid Annual Leave" )]
              public byte? PaidLeaveInDays { get; set; }

              /// <summary>
              /// Additional Paid Annual Leave In Days 
              /// </summary>
              [Display( Name = "Additional Paid Annual Leave " )]
              public byte? AdditionalPaidAnnualLeaveInDays { get; set; }

              [Display( Name = "Trial Period in Months" )]
              public byte? ProbationInMonths { get; set; }

              [Required]
              [Display( Name = "Notice Period In Days" )]
              public byte? NoticePeriodInDays { get; set; }

              [Display( Name = "Received a Job Description" )]
              public bool? ReceivedAJobDescription { get; set; }

              [Required]
              [DataType( DataType.Date )]
              [Display( Name = "Starting Work Date" )]
              public DateTime? StartingWorkDate { get; set; }

              /// <summary>
              /// Date of receipt of contract
              /// </summary>
              [DataType( DataType.Date )]
              [Display( Name = "Date of Receipt of the Contract" )]
              public DateTime? DateOfReceipt { get; set; }

              [Display( Name = "Employee Id" )]
              public int? EmployeeId { get; set; }

              /// <summary>
              /// Place of conclusion of the employment contract
              /// </summary>
              [Display( Name = "Place of conclusion" )]
              public int? PlaceId { get; set; }

              /// <summary>
              /// Employee work place
              /// </summary>
              [Display( Name = "Employee work place" )]
              public int? WorkPlaceId { get; set; }

              public bool? HasBeenDeleted { get; set; }
       }
}


//public int? AgreementsCount { get; set; }

//public AnnexJobTitleVM? LastAnnex { get; set; }

//[Display( Name = "Job Title" )]
//public string? CurrentJobTitle => ( this.LastAnnex != null ) ? this.LastAnnex.JobTitle : this.JobTitle;

//[Display( Name = "Department" )]
//public string? CurrentDepartmentName =>
//       ( this.LastAnnex != null ) ? this.LastAnnex.DepartmentName : this.DepartmentName;