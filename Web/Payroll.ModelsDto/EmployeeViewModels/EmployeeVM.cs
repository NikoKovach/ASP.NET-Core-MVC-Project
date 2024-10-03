using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class EmployeeVM : ValidateBaseModel
       {
              [Display( Name = "Employee Id" )]
              public int? Id { get; set; }

              [Display( Name = "Person Id" )]
              [Required( ErrorMessage = "The field 'Person Id' is required !" )]
              [Range( 1, long.MaxValue, ErrorMessage = "The field 'Person Id' must be greater than 1 !" )]
              public int PersonId { get; set; }

              [Display( Name = "Company Id" )]
              [Required( ErrorMessage = "The field 'Company Id' is required !" )]
              [Range( 1, long.MaxValue, ErrorMessage = "The field 'Company Id' must be greater than 1 !" )]
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