using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
     public class TypeVacation
     {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(150)]
          public string Type { get; set; }

          public ICollection<Vacation> Vacations { get; set; } = new HashSet<Vacation>();

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
     }
}