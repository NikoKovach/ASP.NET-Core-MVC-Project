using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.PersonViewModels
{
       public class SearchPersonVM : ValidateBaseModel
       {
              [Display( Name = "Id" )]
              [Range( 1, int.MaxValue )]
              public int? PersonId { get; set; }

              public string? FirstName { get; set; }

              public string? MiddleName { get; set; }

              public string? LastName { get; set; }

              public string? FullName { get; set; }

              [Display( Name = "Civil Number" )]
              [StringLength( 10, MinimumLength = 2 )]
              public string? CivilID { get; set; }
       }
}