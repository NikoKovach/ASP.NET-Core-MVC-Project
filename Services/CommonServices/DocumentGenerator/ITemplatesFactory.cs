namespace LegalFramework.Services.DocumentGenerator
{
    public interface ITemplatesFactory
    {
        IDictionary<string, ITemplate> Documents { get; set; }
    }
}
