using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator.Templates
{
    public class LaborContractEngPdfTemplate : ITemplate
    {
        public LaborContractEngPdfTemplate( )
        {
            BuildTemplate( );
        }

        public Document Document { get; }
        public IDictionary<string, Dictionary<string, Paragraph>> SectionsDictionary { get; set; }

        //###################################################################

        private void BuildTemplate( )
        {
            //TODO :
        }

    }
}
