namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class ContractView : EmployeeBaseView
       {
              public string? FullName { get; set; }

              public ContractSubView? Contract { get; set; }
       }
}