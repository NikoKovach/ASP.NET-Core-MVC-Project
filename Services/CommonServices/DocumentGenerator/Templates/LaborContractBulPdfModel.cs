using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator.Templates
{
       public class LaborContractBulPdfModel : IDocumentTemplate
       {
              public LaborContractBulPdfModel()
              {
                     BuildTemplate();
              }


              public Document ContractTemplate { get; set; }

              //########################################################

              private void BuildTemplate()
              {
                     Document? document = new Document();

                     this.DocumentStyle( document );

                     {
                            Section? sectionH1 = document.AddSection();

                            this.DocumentPageSetup( sectionH1 );

                            // Add a paragraph to the section.
                            Paragraph? paraContract = sectionH1.AddParagraph( "ТРУДОВ ДОГОВОР" );
                            Paragraph? paraContractNumber = sectionH1.AddParagraph( "№ <<___>>/<<___>> г." );
                     }


                     this.ContractTemplate = document;
              }

              private void DocumentPageSetup( Section? mainSection )
              {
                     mainSection.PageSetup.PageFormat = PageFormat.A4;
                     mainSection.PageSetup.Orientation = Orientation.Portrait;
                     mainSection.PageSetup.TopMargin = Unit.FromMillimeter( 20 );
                     mainSection.PageSetup.RightMargin = Unit.FromMillimeter( 20 );
                     mainSection.PageSetup.LeftMargin = Unit.FromMillimeter( 20 );
                     mainSection.PageSetup.BottomMargin = Unit.FromMillimeter( 10 );
              }

              private void DocumentStyle( Document? document )
              {
                     Style? docStyle = document.Styles[ StyleNames.Normal ];
                     docStyle.Font.Size = Unit.FromPoint( 10 );
                     docStyle.Font.Name = "Arial";
              }
       }
}