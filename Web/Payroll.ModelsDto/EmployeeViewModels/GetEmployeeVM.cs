
using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.EmployeeViewModels
{
       public class GetEmployeeVM : BaseEmployeeVM
       {
              [Display( Name = "List number" )]
              public string? NumberFromTheList { get; set; }

              [Display( Name = "Is Active" )]
              public bool IsPresent { get; set; }

              public PersonEmpVM? Person { get; set; }

              public ContactsEmpVM? ContactInfo { get; set; }

              public IdDocumentEmpVM? IdCardPassport { get; set; }

              public ContractEmpVM? ContractInfo { get; set; }

              public WorkExperienceVM Experience { get; set; }
       }
}

