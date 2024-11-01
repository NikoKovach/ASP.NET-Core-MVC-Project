using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using Payroll.ViewModels.CustomValidation;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
       public class AddressVM : AddressEmpVM
       {
              [Display( Name = "Id" )]
              public int Id { get; set; }

              [StringLength( 100, MinimumLength = 3 )]
              [Display( Name = "Address Type" )]
              [Ignore]
              [Order( 23 )]
              public string? AddressType { get; set; }

              [Display( Name = "Deleted" )]
              [Order( 25 )]
              public bool HasBeenDeleted { get; set; }

              [Required]
              [Display( Name = "Person Id" )]
              [Ignore]
              [Order( 27 )]
              public int? PersonId { get; set; }
       }
}
