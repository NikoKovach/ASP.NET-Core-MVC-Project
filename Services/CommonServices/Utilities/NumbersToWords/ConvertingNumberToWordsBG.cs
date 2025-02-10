namespace LegalFramework.Services.Utilities.NumbersToWords
{
	public class ConvertingNumberToWordsBG : ConvertingNumberToWords, IConvertingNumberToWords
	{
		private readonly IBasicNumericCollectionsBG numericCollectionsBG;

		public ConvertingNumberToWordsBG() : this(new BasicNumericCollectionsBG())
		{ }

		public ConvertingNumberToWordsBG(IBasicNumericCollectionsBG numericCollections)
		{
			this.numericCollectionsBG = numericCollections;
		}

		public override string? WriteNumberInWords(decimal number, string resultType)
		{
			this.WholePartStr = this.ParseWholePart(this.GetWholePart(number), resultType);

			this.FractionPartStr = this.ParseFractionPart(this.GetFractionalPart(number));

			if (string.IsNullOrEmpty(this.WholePartStr) || string.IsNullOrEmpty(this.FractionPartStr))
				return default;

			return this.JoinNumberInWords(resultType);
		}

		protected override string ParseWholePart(long wholePart, string resultType)
		{
			//wholePart = 2502000002;
			//resultType = "currency";

			int digitsCount = this.CountTheNumberOfDigits(wholePart);

			int numberOfTriples = (int)Math.Ceiling(digitsCount / (double)NumericConstants.Three);

			string result = string.Empty;

			for (int i = numberOfTriples; i >= NumericConstants.One; i--)
			{
				long remainingValue = this.SetRemainingValue(wholePart, i);

				int threesome = this.SetThreesome(remainingValue, i);

				if (threesome == NumericConstants.Zero) continue;

				Dictionary<int, int> degreeDictionary = this.SetDegreeDictionary(threesome);

				if (string.IsNullOrEmpty(result))
				{
					result = this.ConvertNumberInWords(degreeDictionary, resultType, i);

					continue;
				}

				string lastAnd = this.SetLastAnd(remainingValue, i, degreeDictionary);

				result += lastAnd + this.ConvertNumberInWords(degreeDictionary, resultType, i);
			}

			return result;
		}

		protected override string ParseFractionPart(decimal fractionalPart)
		{
			if (fractionalPart <= NumericConstants.Zero) return string.Empty;

			return NumberConstantsBG.Zero;
		}

		protected override string JoinNumberInWords(string resultType)
		{
			string middleText = (resultType.Equals("number")) ? " цяло и " : " лв. и ";

			return $"{this.WholePartStr}{middleText}{this.FractionPartStr}";
		}

		//##########################################################################

		protected override string ConvertNumberInWords(Dictionary<int, int> listOfNumbers,
													string resultType, int numberOfTriples)
		{
			this.TextBuilder.Clear();

			int firstDigit = listOfNumbers.Values.ElementAt(0);

			if (listOfNumbers.Count == NumericConstants.One && firstDigit != NumericConstants.Zero)
			{
				this.TextBuilder.Append(this.FirstDigitByResultType(resultType, firstDigit));

				this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit);

				return this.TextBuilder.ToString();
			}

			//################################################################
			int theTen = listOfNumbers.Values.ElementAt(NumericConstants.One);

			if (listOfNumbers.Count == NumericConstants.Two
				&& (theTen + firstDigit) < NumericConstants.Twenty)
			{
				this.TextBuilder.Append(this.numericCollectionsBG.TenToNineteenDic[theTen + firstDigit]);

				this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit, theTen);

				return this.TextBuilder.ToString();
			}


			if (listOfNumbers.Count == NumericConstants.Two)
			{
				this.TextBuilder.Append(this.numericCollectionsBG.TensDic[theTen]);

				if (firstDigit > NumericConstants.Zero)
				{
					this.TextBuilder.Append($" и {FirstDigitByResultType(resultType, firstDigit)}");
				}

				this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit, theTen);

				return this.TextBuilder.ToString();
			}

			//#################################################################
			int hundreds = listOfNumbers.Values.ElementAt(NumericConstants.Two);

			this.TextBuilder.Append(this.numericCollectionsBG.HundredsDic[hundreds]);

			if (firstDigit == NumericConstants.Zero && theTen >= NumericConstants.Twenty)
			{
				this.TextBuilder.Append($" и {this.numericCollectionsBG.TensDic[theTen]}");

				this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit, theTen, hundreds);

				return this.TextBuilder.ToString();
			}

			if (theTen == NumericConstants.Zero && firstDigit > NumericConstants.Zero)
			{
				this.TextBuilder.Append($" и {this.FirstDigitByResultType(resultType, firstDigit)}");

				this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit, theTen, hundreds);

				return this.TextBuilder.ToString();
			}

			if (theTen > NumericConstants.Zero && theTen < NumericConstants.Twenty)
			{
				this.TextBuilder
					.Append($" и {this.numericCollectionsBG.TenToNineteenDic[theTen + firstDigit]}");

				this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit, theTen, hundreds);

				return this.TextBuilder.ToString();
			}

			if (theTen >= NumericConstants.Twenty)
			{
				this.TextBuilder.Append($" {this.numericCollectionsBG.TensDic[theTen]} " +
								$"и {this.FirstDigitByResultType(resultType, firstDigit)}");
			}

			this.AddPostfixOfTheTripleDegree(numberOfTriples, firstDigit, theTen, hundreds);

			return this.TextBuilder.ToString();
		}

		protected override void AddPostfixOfTheTripleDegree(int numberOfTriples,
															int firstDigit = default,
															int theTen = default,
															int hundreds = default)
		{
			if (numberOfTriples == NumericConstants.One) return;

			string tripleDegreeStr = this.numericCollectionsBG.TripleDegrees[numberOfTriples];


			if (firstDigit <= NumericConstants.Two &&
				theTen == NumericConstants.Zero &&
				hundreds == NumericConstants.Zero)
			{
				if (firstDigit == NumericConstants.One && numberOfTriples == NumericConstants.Two)
				{
					this.TextBuilder.Clear();

					this.TextBuilder.Append(NumberConstantsBG.OneThousand);

					return;
				}

				if (firstDigit == NumericConstants.Two && numberOfTriples == NumericConstants.Two)
				{
					this.TextBuilder.Clear();

					this.TextBuilder.Append($"{NumberConstantsBG.Two} {NumberConstantsBG.PostfixThousands}");

					return;
				}

				if (numberOfTriples == NumericConstants.Three)
				{
					string newValue = (firstDigit == NumericConstants.One)
									  ? NumberConstantsBG.OneMillion
									  : $"{NumberConstantsBG.TwoMaleKind} {NumberConstantsBG.PostfixMillion}";

					this.TextBuilder.Clear();

					this.TextBuilder.Append(newValue);

					return;
				}

				if (numberOfTriples == NumericConstants.Four)
				{
					string newValue = (firstDigit == NumericConstants.One)
									  ? NumberConstantsBG.OneBillion
									  : $"{NumberConstantsBG.TwoMaleKind} {NumberConstantsBG.PostfixBillion}";

					this.TextBuilder.Clear();

					this.TextBuilder.Append(newValue);

					return;
				}
			}

			this.TextBuilder.Append($" {tripleDegreeStr}");
		}

		//#########################################################################

		private string FirstDigitByResultType(string resultType, int firstDigit)
		{
			if (resultType.Equals("currency"))
			{
				if (firstDigit == NumericConstants.One)
				{
					return NumberConstantsBG.OneMaleKind;
				}

				if (firstDigit == NumericConstants.Two)
				{
					return NumberConstantsBG.TwoMaleKind;
				}
			}

			return this.numericCollectionsBG.ZeroToNineDic[firstDigit];
		}

		private string SetLastAnd(long remainingValue, int i, Dictionary<int, int> degreeDictionary)
		{
			if (i > NumericConstants.One)
			{
				string lastAnd = this.SetLastAndThousandthsAreNull(remainingValue, i);

				if (string.IsNullOrEmpty(lastAnd)) return " ";

				return lastAnd;
			}

			if (degreeDictionary.Count == NumericConstants.One) return " и ";

			int firstDigit = degreeDictionary.Values.ElementAt(NumericConstants.Zero);

			int theTen = degreeDictionary.Values.ElementAt(NumericConstants.One);

			if (degreeDictionary.Count == NumericConstants.Two &&
							firstDigit == NumericConstants.Zero) return " и ";

			if (degreeDictionary.Count == NumericConstants.Two &&
				(firstDigit > NumericConstants.Zero && theTen < NumericConstants.Twenty)) return " и ";

			if (degreeDictionary.Count == NumericConstants.Three)
			{
				if (firstDigit == NumericConstants.Zero && theTen == NumericConstants.Zero) return " и ";
			}

			return " ";
		}

		private string SetLastAndThousandthsAreNull(long remainingValue, int index)
		{
			List<int> threesomeList = new List<int>();

			for (int i = NumericConstants.One; i < index; i++)
			{
				long value = this.SetRemainingValue(remainingValue, i);

				int threesome = this.SetThreesome(value, i);

				threesomeList.Add(threesome);
			}

			if (threesomeList.All(x => x == NumericConstants.Zero)) return " и ";

			return string.Empty;
		}
	}
}

/*
//int value = (int)(wholePart %
//			(int)Math.Pow(NumericConstants.Ten, NumericConstants.Three * i));

//int threeNumbers = value / (int)Math
//							   .Pow(NumericConstants.Ten, NumericConstants.Three * (i - 1));
 
 
			//this.TextBuilder.Append($" {this.numericCollectionsBG.TensDic[theTen]} " +
			//					$"и {this.FirstDigitByResultType(resultType, firstDigit)}");
			//$"," +


			//if (digitsCount == NumericConstants.Three)
			//{
			//	int degreeBaseTen = (int)Math.Pow(NumericConstants.Ten, NumericConstants.Two);

			//	int theHundreds = wholePart / degreeBaseTen * degreeBaseTen;

			//	int theTen = (wholePart % degreeBaseTen) / NumericConstants.Ten * NumericConstants.Ten;

			//	int firstDigit = wholePart % NumericConstants.Ten;

			//	return NumbersGreaterThenTwentyInWords(firstDigit, theTen, theHundreds);
			//}
 */
