
using LegalFramework.Services.DocumentGenerator.FillWayTemplates;

namespace LegalFramework.Services.DocumentGenerator
{
       public class FillWayFactory : IFillWayStore
       {
              public FillWayFactory()
              {
                     this.FillWayStore = new Dictionary<string, IFill>()
                     {
                            { "laborContract-eng-pdf" , new FillLaborContract()},
                     };
              }

              public IDictionary<string, IFill> FillWayStore { get; set; }
       }
}
