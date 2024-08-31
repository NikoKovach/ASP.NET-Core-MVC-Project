using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;

namespace Payroll.ViewModels
{
       public class SearchCompanyVM
       {
              public int Id { get; set; }

              public string Name { get; set; }

              [Display( Name = "Unique Identifier" )]
              public string UniqueIdentifier { get; set; }

              [Ignore]
              [Display( Name = "Companies List" )]
              public string Info => $"{Name} -> UniqueId : {UniqueIdentifier}";
       }
}
