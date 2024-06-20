using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models.EnumTables
{
    public class Gender
    {
          [Key]
          public int Id { get; set; }

          [Required]
          [StringLength(20, MinimumLength = 1)]
          public string Type { get; set; }

          public ICollection<Person> Persons { get; set; } = new HashSet<Person>();

          public ICollection<TemporaryDisability> TemporaryDisabilities { get; set; } = new HashSet<TemporaryDisability>();
    }
}