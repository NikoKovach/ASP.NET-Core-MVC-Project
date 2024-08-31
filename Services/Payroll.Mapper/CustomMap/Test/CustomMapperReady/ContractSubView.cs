using System.ComponentModel.DataAnnotations;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class ContractSubView
       {
              //public ContractSubView()
              //{
              //       this.Agreements = new List<ContractSubAnnexes>();
              //}

              [Display( Name = "Job title" )]
              public string? JobTitle { get; set; }

              [Display( Name = "Department" )]
              public string? DepartmentName { get; set; }

              [Display( Name = "Contract type" )]
              public string? ContractType { get; set; }

              [Display( Name = "Contract number" )]
              public string? ContractNumber { get; set; }

              [DataType( DataType.Date )]
              [Display( Name = "Contract date" )]
              public DateTime? ContractDate { get; set; }

              //public int AgreementsCount => this.Agreements.Count;

              public ContractSubAnnexe? LastAgreement { get; set; }
              //public List<ContractSubAnnexes> Agreements { get; set; }
       }
}
