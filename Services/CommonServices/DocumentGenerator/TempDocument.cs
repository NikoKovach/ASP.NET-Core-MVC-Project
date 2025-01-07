using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Fields;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using PdfSharp.Quality;

namespace LegalFramework.Services.DocumentGenerator
{
       public class TempDocument : ITempDocument
       {
              private const string DefaultFilePath = @"D:\SoftUni Courses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\assets";

              private IDictionary<string, Document?> documentTemplates;
              private IDictionary<string, IFill> fillWayStore;

              public TempDocument() : this( DefaultTemplates(), DefaultFillWays() )
              { }

              public TempDocument( ITemplatesFactory templates, IFillWayStore fillWayStore )
              {
                     this.documentTemplates = templates.DocumentTemplates;

                     this.fillWayStore = fillWayStore.FillWayStore;
              }

              public string? CreateFile( string? path, string? documentType,
                                                          object? documentModel = default, string fileType = "pdf", string language = "bul" )
              {
                     string filePath = ( !string.IsNullOrEmpty( path ) ) ? path : DefaultFilePath;

                     string fileName = ( !string.IsNullOrEmpty( path ) ) ? this.NewName( documentType ) : this.NewName();

                     string tempFilePath = @$"{filePath}\{fileName}.{fileType}";

                     string templateType = $@"{documentType}-{language}-{fileType}";
                     //"laborContract-eng-pdf"

                     if ( string.IsNullOrEmpty( path ) || string.IsNullOrEmpty( documentType ) )
                     {
                            this.CreateDefault( tempFilePath );
                     }
                     else
                     {
                            this.CreateNewFile( tempFilePath, templateType, documentModel );
                     }

                     if ( !File.Exists( tempFilePath ) )
                            return string.Empty;

                     return tempFilePath;
              }

              //########################################################

              private void CreateNewFile( string tempFilePath, string? templateType, object? documentModel )
              {
                     if ( string.IsNullOrEmpty( templateType ) )
                            throw new InvalidOperationException( $"Parameter '{nameof( templateType )}' is empty or null !" );

                     Document? document = this.documentTemplates[ templateType ] ??
                            throw new InvalidOperationException( $"Template with name '{templateType}' is empty or null !" );

                     //bool fillingIsSuccessful = this.fillWayStore[ templateType ].Fill( document, documentModel );

                     //if ( !fillingIsSuccessful )
                     //       throw new InvalidOperationException( $"The fill operation was unsuccessful !" );

                     SaveDocument( tempFilePath, document );
              }

              public void CreateDefault( string filePath = DefaultFilePath )
              {
                     Document? document = CreateDocument();

                     Style? style = document.Styles[ StyleNames.Normal ]!;
                     style.Font.Name = "Arial";

                     SaveDocument( filePath, document );
              }

              private static Document CreateDocument()
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
                     paragraph.AddFormattedText( "Hello, World!", TextFormat.Bold );

                     // Create the primary footer.
                     var footer = section.Footers.Primary;

                     // Add content to footer.
                     paragraph = footer.AddParagraph();
                     paragraph.Add( new DateField { Format = "yyyy/MM/dd HH:mm:ss" } );
                     paragraph.Format.Alignment = ParagraphAlignment.Center;

                     // Add MigraDoc logo.
                     string imagePath = IOUtility.GetAssetsPath( @"migradoc/images/MigraDoc-128x128.png" )!;
                     document.LastSection.AddImage( imagePath );

                     return document;
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
                     pdfRenderer.RenderDocument();

                     // Save the document...
                     pdfRenderer.PdfDocument.Save( filePath );
              }

              private string NewName( string documentType = "Document" )
              {
                     string dateFormat = "yyyyMMdd-Hmmss";

                     return $"{documentType}-{DateTime.UtcNow.ToString( dateFormat )}";
              }

              private static ITemplatesFactory DefaultTemplates() => new DocumentTemplatesConfig();

              private static IFillWayStore DefaultFillWays() => new FillWayFactory();
       }
}

//Style? style = document.Styles[ StyleNames.Normal ]!;
//style.Font.Name = "Arial";