using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{

       public partial class Department
       {
              [Key]
              public int DepartmentID { get; set; }

              [Required]
              [StringLength( 100, MinimumLength = 3 )]
              public string Name { get; set; }

              public ICollection<EmploymentContract> EmploymentContracts { get; set; } = new HashSet<EmploymentContract>();

              public ICollection<Annex> Annexes { get; set; } = new HashSet<Annex>();

              public bool HasBeenDeleted { get; set; }

              public DateTime? DeletionDate { get; set; }
       }
}
