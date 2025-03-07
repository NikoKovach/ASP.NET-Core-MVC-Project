namespace LegalFramework.Services.NumbersToWords
{
    public interface IConvertingNumberToWords
    {
        string? WriteNumberInWords( decimal? number,
                                    string resultType = SupportConstants.Number,
                                    string cultureName = SupportConstants.CultureName );
        string? WriteNumberInWords( int? number,
                                    string resultType = SupportConstants.Number,
                                    string cultureName = SupportConstants.CultureName );

        string? WriteNumberInWords( double? number,
                                    string resultType = SupportConstants.Number,
                                    string cultureName = SupportConstants.CultureName );

        IBasicNumericCollections NumericCollections { get; }
    }
}
