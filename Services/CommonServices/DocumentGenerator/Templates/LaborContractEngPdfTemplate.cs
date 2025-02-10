using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator.Templates
{
	public class LaborContractEngPdfTemplate : ITemplate
	{
		public LaborContractEngPdfTemplate()
		{
			BuildTemplate();
		}

		public Document Document { get; }

		IDictionary<string, Dictionary<string, Paragraph>> ITemplate.SectionsDic { get; set; }

		//###################################################################

		private void BuildTemplate()
		{
			//TODO :
		}

	}
}
