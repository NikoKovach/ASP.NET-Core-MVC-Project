using System.ComponentModel.DataAnnotations;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
       public class AddressVM : AddressEmpVM
       {
              public int Id { get; set; }

              public int? PersonId { get; set; }

              [StringLength( 100, MinimumLength = 3 )]
              public string? AddressType { get; set; }

              public bool HasBeenDeleted { get; set; }
       }
}
