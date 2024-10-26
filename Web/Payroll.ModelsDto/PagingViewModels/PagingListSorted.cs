namespace Payroll.ViewModels.PagingViewModels
{
       public class PagingListSorted<T> : PagingBaseList<T>
              where T : new()
       {
              public PagingListSorted( List<T> items, int maxTotalPagest, int pageIndex, int pageSize, string? sortValue ) :
                     base( items, maxTotalPagest, pageIndex, pageSize )
              {
                     this.Entity = new T();

                     this.SortValue = sortValue;
              }

              public string? SortValue { get; set; }

              public T Entity { get; set; }

              public static async Task<PagingListSorted<T>> CreateSortedCollectionAsync
              ( IQueryable<T> source, int pageIndex, int? pageSize, string? sortParam )
              {
                     int tableRowsCount = ( pageSize < 1 ) ? 1 : pageSize ?? 1;

                     int totalPages = await GetTotalPages( source, tableRowsCount );

                     int validPageIndex = GetPageIndex( totalPages, pageIndex );

                     List<T> items = await GetItemsList( source, validPageIndex, tableRowsCount );

                     return new PagingListSorted<T>( items, totalPages, validPageIndex, tableRowsCount, sortParam );
              }
       }
}
