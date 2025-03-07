using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Fields;

using PdfSharp.Quality;

namespace LegalFramework.Services.DocumentGenerator.Templates
{
    public class DefaultPdfTemplate : ITemplate
    {
        public DefaultPdfTemplate( )
        {
            BuildTemplate( );
        }

        public Document Document { get; private set; }

        public IDictionary<string, Dictionary<string, Paragraph>> SectionsDictionary { get; set; }

        //########################################################

        private void BuildTemplate( )
        {
            // Create a new MigraDoc document.
            this.Document = new Document( );
            DocumentStyle( this.Document );

            // Add a section to the document.
            Section section = this.Document.AddSection( );
            DocumentPageSetup( section );

            // Add a paragraph to the section.
            Paragraph paragraph = section.AddParagraph( );

            // Set font color.
            paragraph.Format.Font.Color = Colors.DarkBlue;

            // Add some text to the paragraph.
            paragraph.AddFormattedText( "Hello, World!", TextFormat.Bold );

            // Create the primary footer.
            HeaderFooter footer = section.Footers.Primary;

            // Add content to footer.
            paragraph = footer.AddParagraph( );
            paragraph.Add( new DateField { Format = "yyyy/MM/dd HH:mm:ss" } );
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Add MigraDoc logo.
            string imagePath = IOUtility.GetAssetsPath( @"migradoc/images/MigraDoc-128x128.png" )!;
            this.Document.LastSection.AddImage( imagePath );

            this.SectionsDictionary = new Dictionary<string, Dictionary<string, Paragraph>>( )
            {
                {"mainSection",new Dictionary<string, Paragraph>()
                               {
                                    {"firstParagraph" ,paragraph},
                               }
                }
            };
        }

        private void DocumentPageSetup( Section? mainSection )
        {
            mainSection.PageSetup.PageFormat = PageFormat.A4;
            mainSection.PageSetup.Orientation = Orientation.Portrait;
            mainSection.PageSetup.TopMargin = Unit.FromMillimeter( 10 );
            mainSection.PageSetup.RightMargin = Unit.FromMillimeter( 15 );
            mainSection.PageSetup.LeftMargin = Unit.FromMillimeter( 15 );
            mainSection.PageSetup.BottomMargin = Unit.FromMillimeter( 10 );
        }

        private void DocumentStyle( Document? document )
        {
            Style? docStyle = document.Styles[StyleNames.Normal];
            docStyle.Font.Size = Unit.FromPoint( 9 );
            docStyle.Font.Name = "Arial";
            document.DefaultTabStop = Unit.FromMillimeter( 7 );
        }
    }
}

/*
		private FormattedText BuildFormattedText(string text)
		{
			FormattedText fText = new FormattedText
			{
				Bold = true,
			};

			fText.AddText(text);

			return fText;
		}
 */