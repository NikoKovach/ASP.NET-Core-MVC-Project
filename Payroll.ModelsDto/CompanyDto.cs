using Payroll.Models;

namespace Payroll.ModelsDto
{
     public class CompanyDto
     {
          public int Id { get; set; }

          public string Name { get; set; }

          public string CompanyHeadquarter { get; set; }

          public string? AddressOfManagement { get; set; }

          public string UniqueIdentifier { get; set; }

          public string? VATRegNumber { get; set; }

          public string RepresentedBy { get; set; }

          public string? RepresentativeIdNumber { get; set; }

          public string? CompanyCaseNumber { get; set; }

          public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

          public bool HasBeenDeleted { get; set; }
     }
}
