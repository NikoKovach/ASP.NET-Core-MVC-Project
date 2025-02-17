namespace LegalFramework.Services.NumbersToWords
{
	public class ConvertingNumberToWordsENG : ConvertingNumberToWords, IConvertingNumberToWords
	{

		private string ParseWholePartEng( int wholePart )
		{
			int digitsCount = CountTheNumberOfDigits( wholePart );



			return "zero";
		}

		private string ParseFractionPartEng( decimal fractionalPart )
		{
			throw new NotImplementedException( );
		}

		private string JoinNumberInWordsEng( string wholePart, string fractionPart )
		{
			throw new NotImplementedException( );
		}
	}
}
