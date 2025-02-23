namespace LegalFramework.Services.DocumentGenerator
{
    public interface IFill
    {
        bool Fill( ITemplate? document, object[]? documentModels );
    }
}