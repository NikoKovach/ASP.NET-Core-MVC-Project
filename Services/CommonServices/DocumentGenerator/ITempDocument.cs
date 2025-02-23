namespace LegalFramework.Services.DocumentGenerator
{
    public interface ITempDocument
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="documentModels"></param>
        /// <param name="fileType"></param>
        /// <param name="language"></param>
        /// <returns>Relative temp file path.</returns>
        public string? CreateFile( string? path, string? documentType,
                                                 object[]? documentModel = default,
                                                 string fileType = "pdf",
                                                 string language = "eng" );
    }
}
