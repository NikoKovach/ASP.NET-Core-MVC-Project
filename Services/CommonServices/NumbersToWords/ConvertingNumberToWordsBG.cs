namespace LegalFramework.Services.NumbersToWords
{
	public class ConvertingNumberToWordsBG : ConvertingNumberToWords, IConvertingNumberToWords
	{

		public ConvertingNumberToWordsBG( ) : this( new BasicNumericCollectionsBG( ) )
		{ }

		public ConvertingNumberToWordsBG( IBasicNumericCollections numericCollections )
		: base( numericCollections )
		{
			FractionalPostfixDic = SetFractionalPostfixDic( );
		}

		protected IDictionary<int, string> FractionalPostfixDic { get; set; }


		protected override string ParseWholePart( long wholePart, string resultType )
		{
			if (wholePart == SupportConstants.Zero)
				return NumericCollections.ZeroToNineDic[SupportConstants.Zero];

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

				string lastAnd = SetLastAnd( remainingValue, i, degreeDictionary );

				result += lastAnd + ConvertNumberInWords( degreeDictionary, resultType, i );
			}

			return result;
		}

		protected override string ParseFractionalPart( decimal fractionalPart, string resultType )
		{
			string result = string.Empty;

			if (resultType.Equals( SupportConstants.Currency ))
			{
				result = Math.Round( fractionalPart, SupportConstants.Two ).ToString( );

				return result;
			}

			decimal fractionalValue = fractionalPart / SupportConstants.OneDecimal;

			int digitsCount = GetFractionalLength( fractionalValue );

			double tenToPowerOf = Math.Pow( SupportConstants.Ten, digitsCount );

			long valueToParse = (int)(fractionalPart * (decimal)tenToPowerOf);

			result = ParseWholePart( valueToParse, resultType );

			SetFractionalPartPostfix( digitsCount, result );

			return TextBuilder.ToString( );
		}

		protected override string JoinNumberInWords( string resultType, string cultureName )
		{
			if (string.IsNullOrEmpty( FractionalPartStr ))
			{
				if (resultType.Equals( SupportConstants.Currency ))
					return WholePartStr + " лв.";

				return $"{WholePartStr}";
			}

			string middleText = resultType.Equals( SupportConstants.Number ) ? " цяло и " : " лв. и ";

			if (resultType.Equals( SupportConstants.Currency ))
				FractionalPartStr += " ст.";

			return $"{WholePartStr}{middleText}{FractionalPartStr}";
		}

		//##########################################################################

		protected override string ConvertNumberInWords( Dictionary<int, int> listOfNumbers,
													string resultType, int numberOfTriples )
		{
			TextBuilder.Clear( );

			int firstDigit = listOfNumbers.Values.ElementAt( 0 );

			if (listOfNumbers.Count == SupportConstants.One && firstDigit != SupportConstants.Zero)
			{
				TextBuilder.Append( FirstDigitByResultType( resultType, firstDigit ) );

				AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit );

				return TextBuilder.ToString( );
			}

			//################################################################
			int theTen = listOfNumbers.Values.ElementAt( SupportConstants.One );

			if (listOfNumbers.Count == SupportConstants.Two
				&& theTen + firstDigit < SupportConstants.Twenty)
			{
				TextBuilder.Append( NumericCollections.TenToNineteenDic[theTen + firstDigit] );

				AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit, theTen );

				return TextBuilder.ToString( );
			}


			if (listOfNumbers.Count == SupportConstants.Two)
			{
				TextBuilder.Append( NumericCollections.TensDic[theTen] );

				if (firstDigit > SupportConstants.Zero)
				{
					TextBuilder.Append( $" и {FirstDigitByResultType( resultType, firstDigit )}" );
				}

				AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit, theTen );

				return TextBuilder.ToString( );
			}

			//#################################################################
			int hundreds = listOfNumbers.Values.ElementAt( SupportConstants.Two );

			TextBuilder.Append( NumericCollections.HundredsDic[hundreds] );

			if (firstDigit == SupportConstants.Zero && theTen >= SupportConstants.Twenty)
			{
				TextBuilder.Append( $" и {NumericCollections.TensDic[theTen]}" );

				AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit, theTen, hundreds );

				return TextBuilder.ToString( );
			}

			if (theTen == SupportConstants.Zero && firstDigit > SupportConstants.Zero)
			{
				TextBuilder.Append( $" и {FirstDigitByResultType( resultType, firstDigit )}" );

				AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit, theTen, hundreds );

				return TextBuilder.ToString( );
			}

			if (theTen > SupportConstants.Zero && theTen < SupportConstants.Twenty)
			{
				TextBuilder
					.Append( $" и {NumericCollections.TenToNineteenDic[theTen + firstDigit]}" );

				AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit, theTen, hundreds );

				return TextBuilder.ToString( );
			}

			if (theTen >= SupportConstants.Twenty)
			{
				TextBuilder.Append( $" {NumericCollections.TensDic[theTen]} " +
								$"и {FirstDigitByResultType( resultType, firstDigit )}" );
			}

			AddPostfixOfTheTripleDegree( numberOfTriples, firstDigit, theTen, hundreds );

			return TextBuilder.ToString( );
		}

		protected override void AddPostfixOfTheTripleDegree( int numberOfTriples,
															int firstDigit = default,
															int theTen = default,
															int hundreds = default )
		{
			if (numberOfTriples == SupportConstants.One) return;

			string tripleDegreeStr = NumericCollections.TripleDegrees[numberOfTriples];


			if (firstDigit <= SupportConstants.Two &&
				theTen == SupportConstants.Zero &&
				hundreds == SupportConstants.Zero)
			{
				if (firstDigit == SupportConstants.One && numberOfTriples == SupportConstants.Two)
				{
					TextBuilder.Clear( );

					TextBuilder.Append( NumberConstantsBG.OneThousand );

					return;
				}

				if (firstDigit == SupportConstants.Two && numberOfTriples == SupportConstants.Two)
				{
					TextBuilder.Clear( );

					TextBuilder.Append( $"{NumberConstantsBG.Two} {NumberConstantsBG.PostfixThousands}" );

					return;
				}

				if (numberOfTriples == SupportConstants.Three)
				{
					string newValue = firstDigit == SupportConstants.One
									  ? NumberConstantsBG.OneMillion
									  : $"{NumberConstantsBG.TwoMaleKind} {NumberConstantsBG.PostfixMillion}";

					TextBuilder.Clear( );

					TextBuilder.Append( newValue );

					return;
				}

				if (numberOfTriples == SupportConstants.Four)
				{
					string newValue = firstDigit == SupportConstants.One
									  ? NumberConstantsBG.OneBillion
									  : $"{NumberConstantsBG.TwoMaleKind} {NumberConstantsBG.PostfixBillion}";

					TextBuilder.Clear( );

					TextBuilder.Append( newValue );

					return;
				}
			}

			TextBuilder.Append( $" {tripleDegreeStr}" );
		}

		//#########################################################################

		private string FirstDigitByResultType( string resultType, int firstDigit )
		{
			if (resultType.Equals( "currency" ))
			{
				if (firstDigit == SupportConstants.One)
				{
					return NumberConstantsBG.OneMaleKind;
				}

				if (firstDigit == SupportConstants.Two)
				{
					return NumberConstantsBG.TwoMaleKind;
				}
			}

			return NumericCollections.ZeroToNineDic[firstDigit];
		}

		private string SetLastAnd( long remainingValue, int i, Dictionary<int, int> degreeDictionary )
		{
			if (i > SupportConstants.One)
			{
				string lastAnd = SetLastAndThousandthsAreNull( remainingValue, i );

				if (string.IsNullOrEmpty( lastAnd )) return " ";

				return lastAnd;
			}

			if (degreeDictionary.Count == SupportConstants.One) return " и ";

			int firstDigit = degreeDictionary.Values.ElementAt( SupportConstants.Zero );

			int theTen = degreeDictionary.Values.ElementAt( SupportConstants.One );

			if (degreeDictionary.Count == SupportConstants.Two &&
							firstDigit == SupportConstants.Zero) return " и ";

			if (degreeDictionary.Count == SupportConstants.Two &&
				firstDigit > SupportConstants.Zero && theTen < SupportConstants.Twenty) return " и ";

			if (degreeDictionary.Count == SupportConstants.Three)
			{
				if (firstDigit == SupportConstants.Zero && theTen == SupportConstants.Zero) return " и ";
			}

			return " ";
		}

		private string SetLastAndThousandthsAreNull( long remainingValue, int index )
		{
			List<int> threesomeList = new List<int>( );

			for (int i = SupportConstants.One; i < index; i++)
			{
				long value = SetRemainingValue( remainingValue, i );

				int threesome = SetThreesome( value, i );

				threesomeList.Add( threesome );
			}

			if (threesomeList.All( x => x == SupportConstants.Zero )) return " и ";

			return string.Empty;
		}

		private IDictionary<int, string>? SetFractionalPostfixDic( )
		{
			return new Dictionary<int, string>
			{
				{1,NumberConstantsBG.ManyTenths},
				{2,NumberConstantsBG.ManyHundredths},
				{3,NumberConstantsBG.ManyThousandths},
				{4,NumberConstantsBG.ManyTenThousandths},
				{21,NumberConstantsBG.OneTenth},
				{22,NumberConstantsBG.OneHundredth},
				{23,NumberConstantsBG.OneThousandth},
				{24,NumberConstantsBG.OneTenThousandth},
			};
		}

		private void SetFractionalPartPostfix( int digitsCount, string numberInWords )
		{
			TextBuilder.Clear( );

			string newResult = numberInWords;

			if (numberInWords.EndsWith( NumberConstantsBG.One ) ||
				numberInWords.EndsWith( NumberConstantsBG.OneMaleKind ))
			{
				newResult = numberInWords.Replace( NumberConstantsBG.One, NumberConstantsBG.OneFemaleForm );
			}

			TextBuilder.Append( newResult );

			if (numberInWords.Equals( NumberConstantsBG.One ))
			{
				TextBuilder
					.Append( $" {FractionalPostfixDic[SupportConstants.Twenty + digitsCount]}" );
			}
			else
			{
				TextBuilder.Append( $" {FractionalPostfixDic[digitsCount]}" );
			}
		}
	}
}

/*
//int lastIndex = result.LastIndexOf(NumberConstantsBG.One);

//this.TextBuilder.Append(result.Replace(NumberConstantsBG.One, NumberConstantsBG.OneFemaleForm));

 */
