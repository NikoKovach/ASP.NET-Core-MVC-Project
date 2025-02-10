using LegalFramework.Services.Utilities;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Fields;
using MigraDoc.Rendering;

using PdfSharp.Pdf;

namespace LegalFramework.Services.DocumentGenerator
{
	public class TempDocument : ITempDocument
	{
		private const string DefaultFilePath =
		@"D:\SoftwareCourses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Web\" +
		@"PersonnelWebApp\AppFolder\Temp";

		private readonly IDictionary<string, ITemplate> documentTemplates;
		private readonly IDictionary<string, IFill> fillWayStore;

		public TempDocument() : this(DefaultTemplates(), DefaultFillWays())
		{ }

		public TempDocument(ITemplatesFactory templates, IFillWayStore fillWayStore)
		{
			this.documentTemplates = templates.Documents;

			this.fillWayStore = fillWayStore.FillWayStore;
		}

		public string? CreateFile(string? path, string? documentType,
								  object[]? documentModels = default,
								  string fileType = "pdf", string language = "bul")
		{
			string filePath = (!string.IsNullOrEmpty(path)) ? path : DefaultFilePath;

			string fileName = (!string.IsNullOrEmpty(path)) ? this.NewName(documentType) : this.NewName();

			string tempFilePath = @$"{filePath}\{fileName}.{fileType}";

			string templateType = $@"{documentType}-{language}-{fileType}";//"laborContract-bul-pdf"

			if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(documentType))
			{
				this.CreateDefaultFile(tempFilePath);
			}
			else
			{
				this.CreateNewFile(tempFilePath, templateType, documentModels);
			}

			if (!File.Exists(tempFilePath))
			{
				string fileNotFoundMessage = string.Format(ExceptionStrings.FileNotFoundText, tempFilePath);

				throw new FileNotFoundException(fileNotFoundMessage);
			}

			return tempFilePath;
		}

		//###############################################################################

		private void CreateNewFile(string tempFilePath, string? templateType, object[]? documentModels)
		{
			string invalidParameterStr = string.Format(ExceptionStrings.NullOrEmptyParameter,
																		nameof(templateType));

			if (string.IsNullOrEmpty(templateType))
				throw new InvalidOperationException(invalidParameterStr);

			string invalidTemplateStr = string.Format(ExceptionStrings.TemplateIsNullOrEmpty, templateType);

			ITemplate document = this.documentTemplates[templateType] ??
				   throw new InvalidOperationException(invalidTemplateStr);

			string nullReferenceText = string.Format(ExceptionStrings.NullOrEmptyParameter,
																	nameof(documentModels));

			if (NullOrEmptyDocModelsReference(documentModels))
				throw new NullReferenceException(nullReferenceText);

			bool fillingIsSuccessful = this.fillWayStore[templateType].Fill(document, documentModels);

			if (!fillingIsSuccessful)
				throw new InvalidOperationException(ExceptionStrings.UnsuccessFillOperation);

			SaveDocument(tempFilePath, document.Document);
		}

		private void CreateDefaultFile(string filePath = DefaultFilePath)
		{
			Document? document = CreateDefaultDocument();

			Style? style = document.Styles[StyleNames.Normal]!;
			style.Font.Name = "Arial";

			SaveDocument(filePath, document);
		}

		private static Document CreateDefaultDocument()
		{
			// Create a new MigraDoc document.
			var document = new Document();

			// Add a section to the document.
			var section = document.AddSection();

			// Add a paragraph to the section.
			var paragraph = section.AddParagraph();

			// Set font color.
			paragraph.Format.Font.Color = Colors.DarkBlue;

			// Add some text to the paragraph.
			paragraph.AddFormattedText("Hello, World!", TextFormat.Bold);

			// Create the primary footer.
			var footer = section.Footers.Primary;

			// Add content to footer.
			paragraph = footer.AddParagraph();
			paragraph.Add(new DateField { Format = "yyyy/MM/dd HH:mm:ss" });
			paragraph.Format.Alignment = ParagraphAlignment.Center;

			return document;
		}

		private void SaveDocument(string filePath, Document? document)
		{
			// Create a renderer for the MigraDoc document.
			var pdfRenderer = new PdfDocumentRenderer
			{
				// Associate the MigraDoc document with a renderer.
				Document = document,
				PdfDocument =
				{
                    // Change some settings before rendering the MigraDoc document.
                    PageLayout = PdfPageLayout.SinglePage,
					ViewerPreferences =
					{
						FitWindow = true
					}
				}
			};

			// Layout and render document to PDF.
			pdfRenderer.RenderDocument();

			// Save the document...
			pdfRenderer.PdfDocument.Save(filePath);
		}

		private string NewName(string documentType = "Document")
		{
			string dateFormat = "yyyyMMdd-Hmmss";

			return $"{documentType}-{DateTime.UtcNow.ToString(dateFormat)}";
		}

		private static ITemplatesFactory DefaultTemplates() => new DocumentTemplatesConfig();

		private static IFillWayStore DefaultFillWays() => new FillWayFactory();

		private bool NullOrEmptyDocModelsReference(object[]? documentModels)
		{
			if (documentModels != null) return false;

			if (documentModels.Length > 0) return false;

			return true;
		}
	}
}
