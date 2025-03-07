using System.Globalization;
using System.Text;

namespace LegalFramework.Services.NumbersToWords
{
    public class ConvertingNumberToWords : IConvertingNumberToWords
    {
        private long wholePart;

        private decimal currencyFractionalPart;

        public ConvertingNumberToWords( ) : this( new BasicNumericCollectionsEng( ) )
        { }

        public ConvertingNumberToWords( IBasicNumericCollections collectionsOfNumbers )
        {
            this.wholePart = default;

            NumericCollections = collectionsOfNumbers;

            WholePartStr = string.Empty;

            FractionalPartStr = string.Empty;

            TextBuilder = new StringBuilder( );
        }

        public IBasicNumericCollections NumericCollections { get; private set; }

        protected string WholePartStr { get; set; }

        protected string FractionalPartStr { get; set; }

        protected StringBuilder TextBuilder { get; set; }

        public virtual string? WriteNumberInWords( decimal? number,
                                                   string resultType = SupportConstants.Number,
                                                   string cultureName = SupportConstants.CultureName )
        {
            return ConvertToWords( number, resultType, cultureName );
        }

        public virtual string? WriteNumberInWords( double? number,
                                                    string resultType = SupportConstants.Number,
                                                    string cultureName = SupportConstants.CultureName )
        {
            return ConvertToWords( number, resultType, cultureName );
        }

        public virtual string? WriteNumberInWords( int? number,
                                           string resultType = SupportConstants.Number,
                                           string cultureName = SupportConstants.CultureName )
        {
            return ConvertToWords( number, resultType, cultureName );
        }

        //##############################################################################

        protected string? ConvertToWords( object? number, string resultType, string cultureName )
        {
            try
            {
                if (number == null)
                    return default;

                decimal decimalNumber = Convert.ToDecimal( number );

                if (decimalNumber < 0)
                    return default;

                if (GetCultureInfo( cultureName ) == null)
                    return default;

                return this.StringConvertor( decimalNumber, resultType, cultureName ) ?? default;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException( ex.Message );
            }
        }

        protected virtual string? StringConvertor( decimal number, string resultType, string cultureName )
        {
            this.wholePart = (GetFractionalPart( number ) < SupportConstants.One)
                             ? GetWholePart( number )
                             : GetWholePart( number ) + SupportConstants.One;

            Set_FractionalPartStr( number, resultType );

            WholePartStr = ParseWholePart( this.wholePart, resultType );

            if (string.IsNullOrEmpty( WholePartStr ) && string.IsNullOrEmpty( FractionalPartStr ))
                return default;

            return JoinNumberInWords( resultType, cultureName );
        }

        //#################################################################################
        protected virtual string ParseWholePart( long wholePart, string resultType )
        {
            if (wholePart == SupportConstants.Zero)
                return NumberConstantsEng.Zero;

            int digitsCount = CountTheNumberOfDigits( wholePart );

            int numberOfTriples = (int)Math.Ceiling( digitsCount / (double)SupportConstants.Three );

            string result = string.Empty;

            for (int i = numberOfTriples; i >= SupportConstants.One; i--)
            {
                long remainingValue = SetRemainingValue( wholePart, i );

                int threesome = SetThreesome( remainingValue, i );

                if (threesome == SupportConstants.Zero) continue;

                Dictionary<int, int> degreeDictionary = SetDegreeDictionary( threesome );

                if (string.IsNullOrEmpty( result ))
                {
                    result = ConvertNumberInWords( degreeDictionary, resultType, i );

                    continue;
                }

                result += " " + ConvertNumberInWords( degreeDictionary, resultType, i );
            }

            return result;
        }

        protected virtual string ParseFractionalPart( decimal fractionalPart,
                                                      string resultType = SupportConstants.Number )
        {
            decimal fractionalValue = fractionalPart / SupportConstants.OneDecimal;

            if (fractionalValue == 0)
                return string.Empty;

            List<int> fractionalList = fractionalValue.ToString( )
                                       .Substring( SupportConstants.Two )
                                       .Select( x => int.Parse( x.ToString( ) ) )
                                       .ToList( );

            TextBuilder.Clear( );

            for (int i = 0; i < fractionalList.Count; i++)
            {
                if (fractionalList[i] == SupportConstants.Zero)
                {
                    TextBuilder.Append( NumberConstantsEng.ZeroNought );
                }
                else
                {
                    int key = fractionalList[i];
                    TextBuilder.Append( NumericCollections.ZeroToNineDic[key] );
                }

                if (i != fractionalList.Count - 1)
                    TextBuilder.Append( SupportConstants.Interval );
            }

            return TextBuilder.ToString( );
        }

        protected virtual string JoinNumberInWords( string resultType, string cultureName )
        {
            if (resultType.Equals( SupportConstants.Currency ))
                return CurrencyResultInWords( cultureName );

            if (!string.IsNullOrEmpty( this.WholePartStr ) && string.IsNullOrEmpty( FractionalPartStr ))
                return $"{WholePartStr}";

            if (WholePartStr.Equals( NumberConstantsEng.Zero ) &&
                !string.IsNullOrEmpty( this.FractionalPartStr ))
                return $"{NumberConstantsEng.Point} {FractionalPartStr}";

            return $"{WholePartStr} {NumberConstantsEng.Point} {FractionalPartStr}";
        }

        private string CurrencyResultInWords( string cultureName )
        {
            string currencySymbol = GetCurrencySymbol( cultureName )
                                        ?? SupportConstants.NoCurrencySymbol;

            string subunitSymbol = this.NumericCollections.CoinSubunit.ContainsKey( currencySymbol )
                                    ? this.NumericCollections.CoinSubunit[currencySymbol]
                                    : this.NumericCollections
                                          .CoinSubunit[SupportConstants.NoCurrencySymbol];

            if (this.currencyFractionalPart == SupportConstants.Zero)
                return $"{WholePartStr} {currencySymbol}";

            if (this.currencyFractionalPart > 0.01m)
                subunitSymbol += "s";

            return $"{WholePartStr} {currencySymbol} and " +
                   $"{FractionalPartStr} {subunitSymbol}";

            //$182.40, you could write one hundred eighty-two dollars and fifty cents
        }

        //###############################################################################

        protected virtual string ConvertNumberInWords( Dictionary<int, int> listOfNumbers,
                                                                string resultType, int numberOfTriples )
        {
            TextBuilder.Clear( );

            int firstDigit = listOfNumbers.Values.ElementAt( 0 );

            if (listOfNumbers.Count == SupportConstants.One && firstDigit != SupportConstants.Zero)
            {
                TextBuilder.Append( NumericCollections.ZeroToNineDic[firstDigit] );

                AddPostfixOfTheTripleDegree( numberOfTriples );

                return TextBuilder.ToString( );
            }

            //################################################################

            int theTens = listOfNumbers.Values.ElementAt( SupportConstants.One );

            if (listOfNumbers.Count == SupportConstants.Two
                && theTens + firstDigit < SupportConstants.Twenty)
            {
                TextBuilder.Append( NumericCollections.TenToNineteenDic[theTens + firstDigit] );

                AddPostfixOfTheTripleDegree( numberOfTriples );

                return TextBuilder.ToString( );
            }

            if (listOfNumbers.Count == SupportConstants.Two)
            {
                ConvertTheNumbersLessThenHundred( firstDigit, theTens );

                AddPostfixOfTheTripleDegree( numberOfTriples );

                return TextBuilder.ToString( );
            }

            //#################################################################

            int hundreds = listOfNumbers.Values.ElementAt( SupportConstants.Two );

            TextBuilder
            .Append( $"{NumericCollections.ZeroToNineDic[hundreds / 100]} {NumberConstantsEng.Hundreds}" );

            if (theTens + firstDigit > SupportConstants.Zero &&
                theTens + firstDigit <= SupportConstants.Nine)
            {
                TextBuilder.Append( $" {NumericCollections.ZeroToNineDic[firstDigit]}" );

                AddPostfixOfTheTripleDegree( numberOfTriples );

                return TextBuilder.ToString( );
            }

            if (theTens + firstDigit >= SupportConstants.Ten && theTens + firstDigit < SupportConstants.Twenty)
            {
                TextBuilder.Append( $" {NumericCollections.TenToNineteenDic[theTens + firstDigit]}" );

                AddPostfixOfTheTripleDegree( numberOfTriples );

                return TextBuilder.ToString( );
            }

            if (theTens + firstDigit >= SupportConstants.Twenty)
            {
                ConvertTheNumbersLessThenHundred( firstDigit, theTens, hundreds );

                AddPostfixOfTheTripleDegree( numberOfTriples );

                return TextBuilder.ToString( );
            }

            if (theTens + firstDigit == SupportConstants.Zero)
                AddPostfixOfTheTripleDegree( numberOfTriples );

            return TextBuilder.ToString( );
        }

        protected virtual void AddPostfixOfTheTripleDegree( int numberOfTriples,
                                                            int firstDigit = default,
                                                            int theTens = default,
                                                            int hundreds = default )
        {
            if (numberOfTriples == SupportConstants.One) return;

            string tripleDegreeStr = NumericCollections.TripleDegrees[numberOfTriples];

            TextBuilder.Append( $" {tripleDegreeStr}" );
        }

        protected long GetWholePart( decimal number ) => (long)Math.Truncate( number );

        protected decimal GetFractionalPart( decimal number )
        {
            return Math.Round( number - Math.Truncate( number ), SupportConstants.Four );
        }

        protected int GetFractionalLength( decimal number )
        {
            int i = 0;

            while (Math.Round( number, i ) != number)
                i++;

            return i;
        }

        protected internal int CountTheNumberOfDigits( long number )
        {
            number = Math.Abs( number );
            if (number < 10) return 1;
            if (number < 100) return 2;
            if (number < 1_000) return 3;
            if (number < 10_000) return 4;
            if (number < 100_000) return 5;
            if (number < 1_000_000) return 6;
            if (number < 10_000_000) return 7;
            if (number < 100_000_000) return 8;
            if (number < 1_000_000_000) return 9;
            if (number < 10_000_000_000) return 10;
            if (number < 100_000_000_000) return 11;

            return 12;
        }

        protected Dictionary<int, int> SetDegreeDictionary( int wholePart, int digitsCount = default )
        {
            digitsCount = digitsCount == 0 ? CountTheNumberOfDigits( wholePart ) : digitsCount;

            Dictionary<int, int> unitsDic = new Dictionary<int, int>( );

            for (int i = 0; i < digitsCount; i++)
            {
                int degree = (int)Math.Pow( SupportConstants.Ten, i );

                int value = wholePart / degree * degree;

                int firstDigit = value % (degree * SupportConstants.Ten);

                unitsDic[degree] = firstDigit;
            }

            return unitsDic;
        }

        protected long SetRemainingValue( long basis, int index )
        {
            return (long)(basis % Math.Pow( SupportConstants.Ten, SupportConstants.Three * index ));
        }

        protected int SetThreesome( long value, int index )
        {
            double threesome = value / Math.Pow( SupportConstants.Ten, SupportConstants.Three * (index - 1) );

            return (int)threesome;
        }

        protected string? GetCurrencySymbol( string cultureName )
        {
            CultureInfo? countryCulture = GetCultureInfo( cultureName );

            if (countryCulture == null)
                return default;

            return countryCulture.NumberFormat.CurrencySymbol;
        }

        protected CultureInfo? GetCultureInfo( string cultureName )
        {
            return CultureInfo.GetCultures( CultureTypes.SpecificCultures )
                              .Where( x => x.Name == cultureName )
                              .FirstOrDefault( );
        }

        //########################################################################

        private void ConvertTheNumbersLessThenHundred( int firstDigit, int theTens,
                                                       int hundreds = default )
        {
            if (hundreds > SupportConstants.Zero)
            {
                TextBuilder.Append( ' ' );
            }

            TextBuilder.Append( NumericCollections.TensDic[theTens] );

            if (firstDigit > SupportConstants.Zero)
            {
                TextBuilder.Append( $"-{NumericCollections.ZeroToNineDic[firstDigit]}" );
            }

        }

        private void Set_FractionalPartStr( decimal number, string resultType )
        {
            decimal fractionalPart = (GetFractionalPart( number ) < SupportConstants.One)
                                     ? GetFractionalPart( number )
                                     : SupportConstants.ZeroFractionalPart;

            if (resultType.Equals( SupportConstants.Currency ))
            {
                fractionalPart = Math.Round( fractionalPart, SupportConstants.Two );

                if (fractionalPart == SupportConstants.One)
                {
                    this.wholePart++;

                    FractionalPartStr = SupportConstants.ZeroFractionalPartCurency;
                }

                this.currencyFractionalPart = fractionalPart < 1 ? fractionalPart : 0;

                FractionalPartStr = fractionalPart
                                    .ToString( SupportConstants.CurrencyFormat )
                                    .Substring( SupportConstants.Two );
            }
            else
            {
                FractionalPartStr = ParseFractionalPart( fractionalPart, resultType );
            }
        }
    }
}


//protected string GetCurrencySeparator( string cultureName )
//{
//    CultureInfo? countryCulture = GetCultureInfo( cultureName );

//    if (countryCulture == null)
//        return SupportConstants.Dot;

//    return countryCulture.NumberFormat.CurrencyDecimalSeparator;
//}