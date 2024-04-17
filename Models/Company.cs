using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
     public class Company
     {
          [Key] 
          public int Id { get; set; }

          [Required]
          [StringLength(200,MinimumLength = 1)]
          public string Name { get; set; }

          [Required]
          [StringLength(200,MinimumLength = 3)]
          public string CompanyHeadquarter { get; set; }
          
          [StringLength(200,MinimumLength = 3)]
          public string? AddressOfManagement { get; set; }

          /// <summary>
          /// Single identification code according to the commercial register(Bulstat)
          /// </summary>
          [Required]
          [StringLength(20,MinimumLength = 5)]
          public string UniqueIdentifier { get; set; }

          /// <summary>
          /// VAT registration number
          /// </summary>
          [StringLength(25,MinimumLength = 5)]
          public string? VATRegNumber { get; set; }


          [Required]
          [StringLength(150,MinimumLength = 5)]
          public string RepresentedBy { get; set; }


          [StringLength(25,MinimumLength = 5)]
          public string? RepresentativeIdNumber { get; set; }


          [StringLength(200,MinimumLength = 5)]
          public string? CompanyCaseNumber { get; set; }

          public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

          public bool HasBeenDeleted { get; set; }

     }
}
