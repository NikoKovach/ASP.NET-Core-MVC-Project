using AutoMapper.Configuration.Annotations;

namespace Payroll.ViewModels
{
       public class ValidateBaseModel
       {
              [Ignore]
              public string? ViewTableRow { get; set; }
       }
}
