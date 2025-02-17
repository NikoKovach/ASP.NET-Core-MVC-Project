namespace LegalFramework.Services.NumbersToWords
{
	public interface IConvertingNumberToWords
	{
		string? WriteNumberInWords( decimal number,
									string resultType = "number",
									string cultureName = "fr-FR" );

		IBasicNumericCollections NumericCollections { get; }
	}
}
