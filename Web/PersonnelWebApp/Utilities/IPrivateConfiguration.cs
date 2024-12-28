namespace PersonnelWebApp.Utilities
{
       public interface IPrivateConfiguration
       {
              IConfigurationRoot? PrivateConfig();

              void SetPagingVariables( ref int pageIndex, ref int pageSize, ref int count );

              void SetEmployeePagingVariables( ref int _pageIndex, ref int _pageSize, ref int _count );
       }
}
