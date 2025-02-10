
using System.Text;

namespace LegalFramework.Services.Utilities.NumbersToWords
{
	public class ConvertingNumberToWords : IConvertingNumberToWords
	{
		private readonly IBasicNumericCollectionsEng numericCollectionsEng;

		public ConvertingNumberToWords() : this(new BasicNumericCollectionsEng())
		{ }

		public ConvertingNumberToWords(IBasicNumericCollectionsEng collectionsOfNumbers)
		{

			this.numericCollectionsEng = collectionsOfNumbers;

			this.WholePartStr = string.Empty;

			this.FractionPartStr = string.Empty;

			this.NumberInWords = string.Empty;

			this.TextBuilder = new StringBuilder();
		}

		protected string NumberInWords { get; set; }

		protected string WholePartStr { get; set; }

		protected string FractionPartStr { get; set; }

		protected StringBuilder TextBuilder { get; set; }

		public virtual string? WriteNumberInWords(decimal number, string resultType)
		{
			this.WholePartStr = this.ParseWholePart(this.GetWholePart(number), resultType);

			this.FractionPartStr = this.ParseFractionPart(this.GetFractionalPart(number));

			if (string.IsNullOrEmpty(this.WholePartStr) || string.IsNullOrEmpty(this.FractionPartStr))
				return default;

			return this.JoinNumberInWords(resultType);

		}

		protected virtual string ParseWholePart(long wholePart, string resultType)
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

				//string lastAnd = this.SetLastAnd(remainingValue, i, degreeDictionary);

				//result += lastAnd + this.ConvertNumberInWords(degreeDictionary, resultType, i);
				result += this.ConvertNumberInWords(degreeDictionary, resultType, i);
			}

			return result;
		}

		protected virtual string ParseFractionPart(decimal fractionalPart) => NumberConstantsEng.Zero;

		protected virtual string JoinNumberInWords(string resultType)
		{
			//string middleText = (resultType.Equals("number")) ? " цяло и " : " лв. и ";

			//return $"{this.WholePartStr}{middleText}{this.FractionPartStr}";



			return string.Empty;
		}

		//###############################################################################

		protected virtual string ConvertNumberInWords(Dictionary<int, int> listOfNumbers,
																string resultType, int numberOfTriples)
		{
			this.TextBuilder.Clear();

			return this.TextBuilder.ToString();
		}

		protected virtual void AddPostfixOfTheTripleDegree(int numberOfTriples, int firstDigit = default,
												 int theTen = default, int hundreds = default)
		{
			return;
		}

		//##################################################################

		protected int GetWholePart(decimal number) => (int)Math.Truncate(number);

		protected decimal GetFractionalPart(decimal number) => number - Math.Truncate(number);

		protected int CountTheNumberOfDigits(long number)
		{
			number = Math.Abs(number);
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

		protected Dictionary<int, int> SetDegreeDictionary(int wholePart, int digitsCount = default)
		{
			digitsCount = (digitsCount == 0) ? this.CountTheNumberOfDigits(wholePart) : digitsCount;

			Dictionary<int, int> unitsDic = new Dictionary<int, int>();

			for (int i = 0; i < digitsCount; i++)
			{
				int degree = (int)Math.Pow(NumericConstants.Ten, i);

				int value = wholePart / degree * degree;

				int firstDigit = value % (degree * NumericConstants.Ten);

				unitsDic[degree] = firstDigit;

			}

			return unitsDic;
		}

		protected long SetRemainingValue(long basis, int index)
		{
			return (long)(basis % Math.Pow(NumericConstants.Ten, NumericConstants.Three * index));
		}

		protected int SetThreesome(long value, int index)
		{
			double threesome = value / Math.Pow(NumericConstants.Ten, NumericConstants.Three * (index - 1));

			return (int)threesome;
		}
	}
}

