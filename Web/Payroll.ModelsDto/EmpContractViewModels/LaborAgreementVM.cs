using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmpContractViewModels
{
       public class LaborAgreementVM : ValidateBaseModel
       {
              [Display( Name = "Contract Id" )]
              public int? Id { get; set; }

              [Display( Name = "Employee Name" )]
              public string? FirstLastName { get; set; }

              [Required]
              [Display( Name = "Is Active" )]
              public bool? IsActive { get; set; }

              [Display( Name = "Contract Type" )]
              public string? ContractType { get; set; }

              [Required]
              [StringLength( 20, MinimumLength = 1 )]
              [Display( Name = "Contract Number" )]
              public string? ContractNumber { get; set; }

              [Required]
              [DataType( DataType.Date )]
              [Display( Name = "Contract Date" )]
              public DateTime? ContractDate { get; set; }

              [StringLength( 20, MinimumLength = 2 )]
              [Display( Name = "Work Experience" )]
              public string? WorkExperience { get; set; }

              /// <summary>
              /// Specialty Work Experience
              /// </summary>
              [StringLength( 20, MinimumLength = 2 )]
              [Display( Name = "SWE" )]
              public string? SpecialtyWorkExperience { get; set; }

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
              [Display( Name = "Job Title" )]
              public string? JobTitle { get; set; }

              //[Required]
              //[StringLength( 100, MinimumLength = 3 )]
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
              [Display( Name = "APAL" )]
              public byte? AdditionalPaidAnnualLeaveInDays { get; set; }

              /// <summary>
              /// Trial Period in Months
              /// </summary>
              [Display( Name = "TP in Months" )]
              public byte? ProbationInMonths { get; set; }

              /// <summary>
              /// Notice Period In Days
              /// </summary>
              [Required]
              [Display( Name = "NP in Days" )]
              public byte? NoticePeriodInDays { get; set; }

              /// <summary>
              /// Received a Job Description
              /// </summary>
              [Display( Name = "RJD" )]
              public bool? ReceivedAJobDescription { get; set; }

              /// <summary>
              ///  Starting Work Date
              /// </summary>
              [Required]
              [DataType( DataType.Date )]
              [Display( Name = "SW Date" )]
              public DateTime? StartingWorkDate { get; set; }

              /// <summary>
              /// Date of receipt of contract
              /// </summary>
              [DataType( DataType.Date )]
              [Display( Name = "Date of RC" )]
              public DateTime? DateOfReceipt { get; set; }

              [Required]
              [Display( Name = "Employee Id" )]
              public int? EmployeeId { get; set; }

              /// <summary>
              /// Place of conclusion of the employment contract
              /// </summary>
              [Display( Name = "PCEC Id" )]
              public int? PlaceId { get; set; }

              /// <summary>
              /// Employee work place
              /// </summary>
              [Display( Name = "EWP Id" )]
              public int? WorkPlaceId { get; set; }

              public bool? HasBeenDeleted { get; set; }

              public string? FirstName { get; set; }

              public string? LastName { get; set; }

              [Required]
              [Display( Name = "Company Id" )]
              public int? CompanyId { get; set; }

              [Required]
              [Display( Name = "Contract Type Id" )]
              public int? ContractTypeId { get; set; }

              [Required]
              [Display( Name = "LCA Id" )]
              public int? LaborCodeArticleId { get; set; }

              [Display( Name = "Department Id" )]
              public int? DepartmentID { get; set; }

              [Required]
              [Display( Name = "Is Registered" )]
              public bool? IsRegistered { get; set; }

       }
}

