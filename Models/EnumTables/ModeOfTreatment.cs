using System.ComponentModel.DataAnnotations;

namespace Payroll.Models.EnumTables
{
    public class ModeOfTreatment
    {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(200)]
          public string ModeName { get; set; }

          public int? TemporaryDisabilityId { get; set; }
          public TemporaryDisability? TemporaryDisability { get; set; }

          public bool HasBeenDeleted { get; set; }
     }
}