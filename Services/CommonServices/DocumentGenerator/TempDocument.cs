using LegalFramework.Services.Utilities;

using MigraDoc.DocumentObjectModel;
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

        public TempDocument( ) : this( DefaultTemplates( ), DefaultFillWays( ) )
        { }

        public TempDocument( ITemplatesFactory templates, IFillWayStore fillWayStore )
        {
            this.documentTemplates = templates.Documents;

            this.fillWayStore = fillWayStore.FillWayStore;
        }

        public string? CreateFile( string? path, string? documentType,
                                   object[]? documentModels = default,
                                   string fileType = "pdf",
                                   string language = "bul" )
        {
            string nullReferenceText = string.Format( ExceptionStrings.NullOrEmptyParameter,
                                                                  nameof( documentModels ) );

            if (documentModels == null || documentModels.Length == 0)
                throw new NullReferenceException( nullReferenceText );

            string filePath = (!string.IsNullOrEmpty( path )) ? path : DefaultFilePath;

            string fileName = (!string.IsNullOrEmpty( path ))
                              ? this.NewName( documentType ?? "DefaultPdfDocument" )
                              : this.NewName( );

            string tempFilePath = @$"{filePath}\{fileName}.{fileType}";

            string templateType = (!string.IsNullOrEmpty( documentType ))
                                  ? $@"{documentType}-{language}-{fileType}"
                                  : "DefaultPdfDocument";

            this.CreateNewFile( tempFilePath, templateType, documentModels );

            if (!File.Exists( tempFilePath ))
            {
                string fileNotFoundMessage = string.Format( ExceptionStrings.FileNotFoundText, tempFilePath );

                throw new FileNotFoundException( fileNotFoundMessage );
            }

            return tempFilePath;
        }

        //###############################################################################

        private void CreateNewFile( string tempFilePath, string? templateType, object[]? documentModels )
        {
            string invalidParameterStr = string.Format( ExceptionStrings.NullOrEmptyParameter,
                                                                        nameof( templateType ) );

            if (string.IsNullOrEmpty( templateType ))
                throw new InvalidOperationException( invalidParameterStr );

            string invalidTemplateStr = string.Format( ExceptionStrings.TemplateIsNullOrEmpty, templateType );

            ITemplate document = this.documentTemplates[templateType] ??
                   throw new InvalidOperationException( invalidTemplateStr );

            bool fillingIsSuccessful = this.fillWayStore[templateType].Fill( document, documentModels );

            if (!fillingIsSuccessful)
                throw new InvalidOperationException( ExceptionStrings.UnsuccessFillOperation );

            SaveDocument( tempFilePath, document.Document );
        }

        private void SaveDocument( string filePath, Document? document )
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
            pdfRenderer.RenderDocument( );

            // Save the document...
            pdfRenderer.PdfDocument.Save( filePath );
        }

        private string NewName( string documentType = "DefaultPdfDocument" )
        {
            string dateFormat = "yyyyMMdd-Hmmss";

            return $"{documentType}-{DateTime.UtcNow.ToString( dateFormat )}";
        }

        private static ITemplatesFactory DefaultTemplates( ) => new DocumentTemplatesConfig( );

        private static IFillWayStore DefaultFillWays( ) => new FillWayFactory( );
    }
}

