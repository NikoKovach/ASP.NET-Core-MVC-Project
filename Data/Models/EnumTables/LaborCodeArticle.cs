using System.ComponentModel.DataAnnotations;

namespace Payroll.Models.EnumTables
{
    public class LaborCodeArticle
    {
          [Key]
          public int Id { get; set; }

          [Required]
          public string Article { get; set; }

          public ICollection<EmploymentContract>? Contracts { get; set; } = new HashSet<EmploymentContract>();
          
          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }
    }
}