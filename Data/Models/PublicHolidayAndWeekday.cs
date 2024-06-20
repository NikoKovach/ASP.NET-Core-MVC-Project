using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
     public class PublicHolidayAndWeekday
     {
          [Key]
          public int Id { get; set; }

          [Column(TypeName = "date")]
          public DateTime? PublicHoliday { get; set; }

          [Column(TypeName = "date")]
          public DateTime? WorkDay { get; set; }
     }
}
