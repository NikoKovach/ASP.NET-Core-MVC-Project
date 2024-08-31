
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

/*
//[Display(Name = "Permanent address")]
//public string? PermanentAddress { get; set; }
////Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>

//[Display(Name = "Current address")]
//public string? CurrentAddress { get; set; }
////Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>
[Display(Name = "Work experience")]
		public string? WorkExperience { get; set; } 
		//Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>

		[Display(Name = "Specialty experience")]
          public string? SpecialtyWorkExperience  { get; set; } 
		//Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>
*/
