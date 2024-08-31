using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class EmployeeVM
       {
              [Display( Name = "Employee Id" )]
              public int? Id { get; set; }

              [Display( Name = "Person Id" )]
              [Required]
              [Range( 1, long.MaxValue )]
              public int PersonId { get; set; }

              [Display( Name = "Company Id" )]
              [Required]
              [Range( 1, long.MaxValue )]
              public int CompanyId { get; set; }

              [Display( Name = "List Number" )]
              public string? NumberFromTheList { get; set; }

              [Display( Name = "Is Present" )]
              public bool IsPresent { get; set; }

              [Ignore]
              [Display( Name = "Employee Picture" )]
              public IFormFile? ProfileImage { get; set; }
       }
}