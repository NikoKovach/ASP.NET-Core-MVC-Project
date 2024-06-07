using System.ComponentModel.DataAnnotations;

namespace Payroll.Models.EnumTables
{
     public class DeductionType
     {
          [Key]
          public int Id { get; set; }

          [Required]
          public int Code { get; set; }

          [Required]
          [StringLength(200,MinimumLength =3)]
          public string Name { get; set; }

		public DeductionElement? DeductionElement { get; set; }

          public bool? HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
     }
}
