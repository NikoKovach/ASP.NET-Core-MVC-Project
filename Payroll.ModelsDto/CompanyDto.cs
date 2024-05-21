using Payroll.Models;
using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto
{ 
     public class CompanyDto
     {
          public int Id { get; set; }

          [Display(Name ="Company Name")]
          [Required(ErrorMessage = "Pleace, enter company name !")]
          [StringLength(200,MinimumLength = 3,ErrorMessage ="Text with lenght between 3 and 200 characters!")]
          public string Name { get; set; }

          [Display(Name ="Company Headquarter")]
          [Required(ErrorMessage ="The field is required.")]
          [StringLength(200,MinimumLength = 3)]
          public string CompanyHeadquarter { get; set; }

          [Display(Name="Address Of Management")]
          [StringLength(200,MinimumLength = 3)]
          public string? AddressOfManagement { get; set; }

          [Display(Name="Unique Identifier")]
          [Required(ErrorMessage ="The field is required.")]
          [StringLength(20,MinimumLength = 5)]
          public string UniqueIdentifier { get; set; }

          [Display(Name="VAT Registration Number")]
          [StringLength(25,MinimumLength = 5)]
          public string? VATRegNumber { get; set; }

          [Display(Name="CEO Name")]
          [Required(ErrorMessage ="The field is required.")]
          [StringLength(150,MinimumLength = 5)]
          public string RepresentedBy { get; set; }

          [Display(Name="CEO Id Number")]
          [StringLength(25,MinimumLength = 5)]
          public string? RepresentativeIdNumber { get; set; }

          [Display(Name="Company Case Number")]
          [StringLength(200,MinimumLength = 5)]
          public string? CompanyCaseNumber { get; set; }

          public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

          //[Display(Name="Active Status")]
          public bool HasBeenDeleted { get; set; }
     }
}
