namespace PersonnelWebApp.Utilities
{
       public class PrivateConfiguration : IPrivateConfiguration
       {
              private readonly IWebHostEnvironment env;

              public PrivateConfiguration( IWebHostEnvironment environment )
              {
                     this.env = environment;
              }

              public IConfigurationRoot? PrivateConfig()
              {
                     string appFolderPath = this.env.ContentRootPath;

                     string jsonFileName = "appsettingsSecond.json";

                     var jsonFilePath = Path.Combine( appFolderPath, jsonFileName );

                     var config = new ConfigurationBuilder().AddJsonFile( jsonFilePath, true, true ).Build();

                     return config;
              }
       }
}
