namespace PersonnelWebApp.Utilities
{
       public interface IPrivateConfiguration
       {
              IConfigurationRoot? PrivateConfig();

              void SetPagingVariables( ref int pageIndex, ref int pageSize, ref int count );
       }
}
