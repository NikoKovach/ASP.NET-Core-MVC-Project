using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class Vacation
     {
          [Key]
          public int Id { get; set; }

          public int? EmployeeId { get; set; }
          public Employee? Employee { get; set; }

          [Required]
          public int? TypeVacationId { get; set; }
          public TypeVacation? TypeVacation { get; set; }

          [Required]
          [Column(TypeName = "date")]
          public DateTime OnLeaveFrom{ get; set; }

          [Required]
          public int CountOfWorkDays{ get; set; }

          public bool HasBeenDeleted { get; set; }

		public DateTime? DeletionDate { get; set; }

          public ICollection<WorkingDaysByMonth> WorkingDaysByMonths { get; set; } = new HashSet<WorkingDaysByMonth>();

     }
}
