namespace LegalFramework.Services.NumbersToWords
{
	public class ConvertingNumberToWordsBG : ConvertingNumberToWords, IConvertingNumberToWords
	{

		public ConvertingNumberToWordsBG( ) : this( new BasicNumericCollectionsBG( ) )
		{ }

		public ConvertingNumberToWordsBG( IBasicNumericCollections numericCollections )
		: base( numericCollections )
		{
			this.FractionalPostfixDic = SetFractionalPostfixDic( );

			this.OnlyOneFromMillionAndUp = SetOnlyOneFromMillionAndUp( );

			this.OneTwoMaleKind = SetOneTwoMaleKind( );
		}

		protected IDictionary<int, string> FractionalPostfixDic { get; set; }

		protected IDictionary<int, string> OnlyOneFromMillionAndUp { get; set; }

		public IDictionary<int, string> OneTwoMaleKind { get; private set; }

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

			if (fractionalValue == 0)
				return string.Empty;

			int digitsCount = GetFractionalLength( fractionalValue );

			double tenToPowerOf = Math.Pow( SupportConstants.Ten, digitsCount );

			long valueToParse = (int)(fractionalPart * (decimal)tenToPowerOf);

			result = ParseWholePart( valueToParse, resultType );

			SetFractionalPartPostfix( digitsCount, result );

			return TextBuilder.ToString( );
		}

		protected override string JoinNumberInWords( string resultType, string cultureName )
		{
			if (resultType.Equals( SupportConstants.Currency ))
			{
				string? currencySymbol = GetCurrencySymbol( cultureName );

				int fractionalPart = int.Parse( FractionalPartStr );

				if (string.IsNullOrEmpty( FractionalPartStr ) || fractionalPart == SupportConstants.Zero)
				{
					return $"{WholePartStr} {currencySymbol}";
				}

				return $"{WholePartStr} {currencySymbol} и " +
					   $"{FractionalPartStr} {SupportConstants.BgCoinSymbol}";
			}

			if (string.IsNullOrEmpty( FractionalPartStr ))
				return $"{WholePartStr}";

			return $"{WholePartStr}{SupportConstants.BgNumberSeparator}{FractionalPartStr}";
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


			if (numberOfTriples == SupportConstants.Two && firstDigit == SupportConstants.One)
			{
				if (theTen == SupportConstants.Zero && hundreds == SupportConstants.Zero)
				{
					TextBuilder.Clear( );

					TextBuilder.Append( NumberConstantsBG.OneThousand );

					return;
				}
			}

			if (numberOfTriples >= SupportConstants.Three && firstDigit == SupportConstants.One &&
				theTen == SupportConstants.Zero && hundreds == SupportConstants.Zero)
			{
				TextBuilder.Clear( );

				TextBuilder.Append( $"{NumberConstantsBG.OneMaleKind} " +
									$"{this.OnlyOneFromMillionAndUp[numberOfTriples]}" );

				return;
			}

			if (numberOfTriples >= SupportConstants.Three &&
				(firstDigit == SupportConstants.One || firstDigit == SupportConstants.Two))
			{
				if (theTen != SupportConstants.Ten)
				{
					string oldValue = this.NumericCollections.ZeroToNineDic[firstDigit];

					string newValue = this.OneTwoMaleKind[firstDigit];

					this.TextBuilder.Replace( oldValue, newValue );
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

		//###################################################################
		private string SetLastAnd( long remainingValue, int i, Dictionary<int, int> degreeDictionary )
		{
			if (i > SupportConstants.One)
			{
				string lastAnd = SetLastAndThousandthsAreNull( remainingValue, i );

				return string.IsNullOrEmpty( lastAnd ) ? SupportConstants.Interval : lastAnd;
			}

			if (degreeDictionary.Count == SupportConstants.One)
				return SupportConstants.BgAndLeftRightInterval;

			int firstDigit = degreeDictionary.Values.ElementAt( SupportConstants.Zero );

			int theTen = degreeDictionary.Values.ElementAt( SupportConstants.One );

			if (degreeDictionary.Count == SupportConstants.Two && firstDigit == SupportConstants.Zero)
				return SupportConstants.BgAndLeftRightInterval;

			if (degreeDictionary.Count == SupportConstants.Two &&
				firstDigit > SupportConstants.Zero && theTen < SupportConstants.Twenty)
				return SupportConstants.BgAndLeftRightInterval;

			if (degreeDictionary.Count == SupportConstants.Three)
			{
				if (firstDigit == SupportConstants.Zero && theTen == SupportConstants.Zero)
					return SupportConstants.BgAndLeftRightInterval;
			}

			return SupportConstants.Interval;
		}

		private string SetLastAndThousandthsAreNull( long remainingValue, int index )
		{
			List<int> threesomeList = new List<int>( );

			for (int i = SupportConstants.One; i <= index; i++)
			{
				long value = SetRemainingValue( remainingValue, i );

				int threesome = SetThreesome( value, i );

				if (i == index)
				{
					Dictionary<int, int> degreeDictionary = SetDegreeDictionary( threesome );
					int firstDigit = degreeDictionary.Values.ElementAt( SupportConstants.Zero );

					if (degreeDictionary.Count > SupportConstants.One)
					{
						int theTens = degreeDictionary.Values.ElementAt( SupportConstants.One );

						if (firstDigit == SupportConstants.Zero ||
							theTens + firstDigit < SupportConstants.Twenty)
						{
							threesomeList.Add( SupportConstants.Zero );
						}
						else
						{
							threesomeList.Add( threesome );
						}
					}

					continue;
				}

				threesomeList.Add( threesome );
			}

			return threesomeList.All( x => x == SupportConstants.Zero )
				   ? SupportConstants.BgAndLeftRightInterval
				   : string.Empty;
		}

		//###################################################################
		private IDictionary<int, string> SetFractionalPostfixDic( )
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

		private IDictionary<int, string> SetOnlyOneFromMillionAndUp( )
		{
			return new Dictionary<int, string>
			{
				{3,NumberConstantsBG.AMillion},
				{4,NumberConstantsBG.ABillion},
			};
		}

		private IDictionary<int, string> SetOneTwoMaleKind( )
		{
			return new Dictionary<int, string>
			{
				{1,NumberConstantsBG.OneMaleKind},
				{2,NumberConstantsBG.TwoMaleKind},
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
