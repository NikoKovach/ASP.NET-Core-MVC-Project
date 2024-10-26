using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class SearchAddressVM : ValidateBaseModel
       {
              [StringLength( 200, MinimumLength = 3 )]
              public string? Country { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string? Region { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string? City { get; set; }

              [StringLength( 200, MinimumLength = 3 )]
              public string? Street { get; set; }

              public int? Number { get; set; }
       }
}
