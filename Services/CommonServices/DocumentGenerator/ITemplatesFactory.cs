using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator
{
       public interface ITemplatesFactory
       {
              IDictionary<string, Document?> DocumentTemplates { get; set; }
       }
}
