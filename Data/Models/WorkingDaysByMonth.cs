using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
     public class WorkingDaysByMonth
     {
          [Key]
          public int Id { get; set; }

          public int? EmployeeId { get; set; }

          public int? VacationId { get; set; }
          public Vacation? Vacation { get; set; }

          /// <summary>
          /// Temporary Disability Id
          /// </summary>
          public int? TemporaryDisabilityId { get; set; }
          public TemporaryDisability? TemporaryDisability { get; set; }

          public byte Month { get; set; }

          public ushort Year { get; set; }

          public byte WorkDays { get; set; }
     }
}
