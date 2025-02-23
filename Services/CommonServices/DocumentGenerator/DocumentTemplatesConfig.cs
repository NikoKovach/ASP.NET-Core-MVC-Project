using LegalFramework.Services.DocumentGenerator.Templates;
using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator
{
       public class DocumentTemplatesConfig : ITemplatesFactory
       {
              public DocumentTemplatesConfig()
              {
			this.Documents = new Dictionary<string, ITemplate>
                     {
                     };
              }

       }


}
