using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class AddressEmpVM : ValidateBaseModel
       {
              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Country { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Region { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string? Municipality { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? City { get; set; }

              [Required]
              [StringLength( 200, MinimumLength = 3 )]
              public string? Street { get; set; }

              [Required]
              public int? Number { get; set; }

              public string? Entrance { get; set; }

              public int? Floor { get; set; }

              [Display( Name = "Apt Number" )]
              public int? ApartmentNumber { get; set; }
       }
}
