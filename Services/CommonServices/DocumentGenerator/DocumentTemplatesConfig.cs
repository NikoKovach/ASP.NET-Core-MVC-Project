using LegalFramework.Services.DocumentGenerator.Templates;
using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator
{
       public class DocumentTemplatesConfig : ITemplatesFactory
       {
              public DocumentTemplatesConfig()
              {
                     this.DocumentTemplates = new Dictionary<string, Document?>()
                     {
                            {"laborContract-eng-pdf", new LaborContractEngPdfModel().ContractTemplate},
                            {"laborContract-bul-pdf" , new LaborContractBulPdfModel().ContractTemplate}
                     };
              }

              public IDictionary<string, Document?> DocumentTemplates { get; set; }
       }


}
