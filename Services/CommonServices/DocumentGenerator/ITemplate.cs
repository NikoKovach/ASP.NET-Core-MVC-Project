using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator
{
    public interface ITemplate
    {
        Document Document { get; }

        IDictionary<string, Dictionary<string, Paragraph>> SectionsDictionary { get; set; }
    }
}
