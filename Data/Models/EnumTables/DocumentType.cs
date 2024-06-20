using System.ComponentModel.DataAnnotations;

namespace Payroll.Models.EnumTables
{
    public class DocumentType
    {
          [Key]
          public int Id { get; set; }

          [Required]
          public string DocumentName { get; set; }

          public ICollection<IdDocument> IdDocuments { get; set; } = new HashSet<IdDocument>();

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }

		public DeductionElement? DeductionElement { get; set; }
    }
}