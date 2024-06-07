using Payroll.Models.EnumTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class TemporaryDisability
     {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(20,MinimumLength = 3)]
          public string SickSheetNumber { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime DateOfIssue { get; set; }

          [Required]
          public int? TypeSickSheetId { get; set; }
          public TypeSickSheet? TypeSickSheet { get; set; }

          [Required]
          public int? GenderId { get; set; }
          public Gender? Gender { get; set; }

          [Required]
          public int? EmployeeId { get; set; }
          public Employee? Employee { get; set; }

          [StringLength(20)]
          public string? AmbulatorySheetNumber { get; set; }

          /// <summary>
          /// Registration number of a medical facility
          /// </summary>
          [StringLength(20)]
          public string? RegNumberMedFacility { get; set; }

          /// <summary>
          /// Medical Centre
          /// </summary>
          [Required]
          [StringLength(200)]
          public string? IssuedBy { get; set; }

          /// <summary>
          /// Name of doctor/type of MAC(Medical Advisory Committee); clinic/department
          /// </summary>
          [Required]
          [StringLength(200)]
          public string? PublisherName { get; set; }

          [StringLength(200)]
          public string? AddressOfThePublisher { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime OnLeaveFrom{ get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime OnLeaveUntil{ get; set; }

          [Required]
          public int AllLeaveOnCalendarDays { get; set; }

          public int? CountOfWorkDays{ get; set; }

          public string? DaysInAWord { get; set; }

          /// <summary>
          /// Diagnosis according to the International Classification of Diseases
          /// </summary>
          [StringLength(20)]
          public string? DiagnosisAccordingToICD { get; set; }

          [StringLength(200)]
          public string? Diagnosis { get; set; }

          [StringLength(200)]
          public string? Reason { get; set; }

          public ICollection<ModeOfTreatment> ModeOfTreatments { get; set; } = new HashSet<ModeOfTreatment>();

		public ICollection<WorkingDaysByMonth> WorkingDaysByMonths { get; set; } = new HashSet<WorkingDaysByMonth>();

          /// <summary>
          /// To appear for examination on a date
          /// </summary>
          [Column(TypeName ="date")]
          public DateTime? DateToNextExamination { get; set; }

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
     }
}
