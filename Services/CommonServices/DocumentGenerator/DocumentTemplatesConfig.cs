
using LegalFramework.Services.DocumentGenerator.Templates;

namespace LegalFramework.Services.DocumentGenerator
{
    public class DocumentTemplatesConfig : ITemplatesFactory
    {
        public DocumentTemplatesConfig( )
        {
            this.Documents = new Dictionary<string, ITemplate>
            {
                { "DefaultPdfDocument", new DefaultPdfTemplate() },
                { "laborContract-bul-pdf", new LaborContractBulPdfTemplate() },
                { "laborContract-eng-pdf", new LaborContractEngPdfTemplate() },
            };
        }

        public IDictionary<string, ITemplate> Documents { get; set; }
    }
}
