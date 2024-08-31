using System.ComponentModel.DataAnnotations;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class ContractSubAnnexe
       {
              public int? AnnexId { get; set; }

              [Display( Name = "Job title" )]
              public string? JobTitle { get; set; }

              [Display( Name = "Department" )]
              public string? DepartmentName { get; set; }
       }
}
