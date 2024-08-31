using System.ComponentModel.DataAnnotations;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class GetEmpPersonView : EmployeeBaseView
       {

              [Display( Name = "Person Id" )]
              public int PersonId { get; set; }

              [Display( Name = "Full Name" )]
              public string? FullName { get; set; }

              [Display( Name = "Gender" )]
              public string? GenderType { get; set; }

              public string EGN { get; set; }
       }
}