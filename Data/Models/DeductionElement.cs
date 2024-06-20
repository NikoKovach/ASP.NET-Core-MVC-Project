using Payroll.Models.EnumTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class DeductionElement
     {
          [Key]
          public int Id { get; set; }

          public int? EmployeeId { get; set; }
          public Employee? Employee { get; set; }

          public int? DeductionTypeId { get; set; }
          public DeductionType? DeductionType { get; set; }

          public float? RateInPercent { get; set; }

          [Column(TypeName ="date")]
          public DateTime? AddedOnDate { get; set; }

          public bool? HasBeenDeleted { get; set; }

          public DateTime? DeletionDate { get; set; }

     }
}
