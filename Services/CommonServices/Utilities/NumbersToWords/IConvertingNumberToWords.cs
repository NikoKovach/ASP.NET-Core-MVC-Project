namespace LegalFramework.Services.Utilities.NumbersToWords
{
	public interface IConvertingNumberToWords
	{
		string? WriteNumberInWords(decimal number, string resultType = "number");
	}
}
