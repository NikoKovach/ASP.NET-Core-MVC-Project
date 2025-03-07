using System.Globalization;
using System.Text;

using LegalFramework.Services.NumbersToWords;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Visitors;

namespace LegalFramework.Services.Utilities
{
    public class FillUtilities
    {
        private readonly static Dictionary<string, IConvertingNumberToWords> numberToWordsConvertor
                                                                            = SetNumberConvertor( );

        public const string TextFromDots = "............";

        public static void AddTextToStringBuilder( Paragraph paragraph, StringBuilder sb )
        {
            sb.Clear( );

            sb.Append( paragraph.GetText( ) );
        }

        public static void AddTextToStringBuilder( DocumentObject paragraphElement, StringBuilder sb )
        {
            sb.Clear( );

            string content = GetElementContent( paragraphElement );

            sb.Append( content );
        }

        public static string NewValue( string? value )
        {
            return !string.IsNullOrWhiteSpace( value ) ? value : TextFromDots;
        }

        public static string NewValue( DateTime? date,
                                       string dateFormat,
                                       string currentCultureName = "bg-BG" )
        {
            if (date == null)
                return TextFromDots;

            CultureInfo currentCulture = CultureInfo.GetCultureInfo( currentCultureName );

            return date.Value
                       .Date
                       .ToString( dateFormat, currentCulture );
        }

        public static string NewValue( int? value )
        {
            if (value == null || value < 1)
                return TextFromDots;

            return value.ToString( );
        }

        public static string NewValue( decimal? value )
        {
            if (value == null || value < 0.000000001m)
                return TextFromDots;

            return value.ToString( );
        }

        public static string NewValue( double? value )
        {
            if (value == null || value < 0.000000001)
                return TextFromDots;

            return value.ToString( );
        }

        public static void FillParagraph( Paragraph paragraph, StringBuilder sb )
        {
            DocumentObject? firstElement = paragraph.Elements.First;

            string exceptionMessage = string.Format( ExceptionStrings.NullableElement,
                                                            nameof( firstElement ) );

            if (firstElement == null)
                throw new NullReferenceException( exceptionMessage );

            firstElement.SetValue( "Content", sb.ToString( ) );
        }

        public static void FillParagraphElement( DocumentObject paragraphElement, StringBuilder sb )
        {
            paragraphElement.SetValue( "Content", sb.ToString( ) );
        }

        public static string GetElementContent( DocumentObject paragraphElement )
        {
            string? text = paragraphElement
                           .GetValue( "Content" )
                           .ToString( );

            return text;
        }

        public static string TheNumberInWords( int? number,
                                               string resultType = "number",
                                               string cultureName = "es-ES" )
        {
            string? numberInWords = numberToWordsConvertor[cultureName]
                                    .WriteNumberInWords( number, resultType, cultureName );

            return numberInWords;
        }

        public static string TheNumberInWords( decimal? number,
                                               string resultType = "number",
                                               string cultureName = "es-ES" )
        {
            string? numberInWords = numberToWordsConvertor[cultureName]
                                    .WriteNumberInWords( number, resultType, cultureName );

            return numberInWords;
        }

        internal static void SetBoldText( Paragraph paragraph, int index,
                                          string oldValue, string newValue )
        {
            var element = paragraph.Elements[index];
            string? elementText = element.GetValue( "Content" ).ToString( );

            int oldValueLenght = oldValue.Length;
            int oldValueIndex = elementText.IndexOf( oldValue );

            string baseTextOne = elementText.Substring( 0, oldValueIndex );
            string baseTextTwo = elementText.Substring( oldValueIndex + oldValueLenght );

            paragraph.Elements[index].SetNull( );

            paragraph.AddText( baseTextOne );
            paragraph.AddFormattedText( newValue, TextFormat.Bold );
            paragraph.AddText( baseTextTwo );
        }

        //###################################################################################

        private static Dictionary<string, IConvertingNumberToWords> SetNumberConvertor( )
        {
            return new Dictionary<string, IConvertingNumberToWords>
            {
                {"bg-BG",new ConvertingNumberToWordsBG() },
                //return international convertor
                {"es-ES",new ConvertingNumberToWordsENG() },
            };
        }
    }
}
