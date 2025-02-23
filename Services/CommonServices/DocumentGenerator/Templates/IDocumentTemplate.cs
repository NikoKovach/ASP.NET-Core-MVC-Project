using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator.Templates
{
       public interface IDocumentTemplate
       {
              Document ContractTemplate { get; set; }
       }
}
