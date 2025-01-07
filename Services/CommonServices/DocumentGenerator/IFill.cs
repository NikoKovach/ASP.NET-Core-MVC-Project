using MigraDoc.DocumentObjectModel;

namespace LegalFramework.Services.DocumentGenerator
{
       public interface IFill
       {
              bool Fill( Document document, object? documentModel );
       }
}