using AutoMapper.Configuration.Annotations;
using Payroll.ViewModels.CustomValidation;

namespace Payroll.ViewModels
{
       public class ValidateBaseModel
       {
              [Ignore]
              [Order( 99 )]
              public string? ViewTableRow { get; set; }
       }
}
