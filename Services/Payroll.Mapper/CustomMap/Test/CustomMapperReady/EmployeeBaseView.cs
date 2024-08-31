using System.ComponentModel.DataAnnotations;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class EmployeeBaseView
       {
              public int Id { get; set; }

              [Display( Name = "Company" )]
              public int? CompanyId { get; set; }

              [Display( Name = "List number" )]
              public string? NumberFromTheList { get; set; }

              [Display( Name = "Is Active" )]
              public bool IsPresent { get; set; }
       }
}

/*
1.Базов клас Employee View Model от например EmployeeVM да е базовия , а другите ViewModels 
да наследяват базовия

Func<IQueryable<Employee>,ViewModel >

или IQueryable<Employee>,IQueryable<Employee>
2.или с екстеншън методи,които да връшат сътветния viewModel на някакъв медод основен медод
*/
