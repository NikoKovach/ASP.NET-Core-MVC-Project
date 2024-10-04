using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class EducationTypeVM
       {
              public int Id { get; set; }

              public string? Type { get; set; }

              [Display( Name = "Deleted" )]
              public bool HasBeenDeleted { get; set; }
       }
}
