using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
     public class TypeSickSheet
     {
          [Key]
          public int Id { get; set; }

          [StringLength(50)]
          public string TypeName { get; set; }

          public ICollection<TemporaryDisability> TemporaryDisabilities { get; set; }

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
     }
}