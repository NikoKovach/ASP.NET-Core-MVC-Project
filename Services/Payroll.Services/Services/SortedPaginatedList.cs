namespace Payroll.Services.Services
{
       public class SortedPaginatedList<T> : PaginatedCollection<T> where T : new()
       {
              public SortedPaginatedList( List<T> items, int maxTotalPagest, int pageIndex, string? sortValue ) :
                     base( items, maxTotalPagest, pageIndex )
              {
                     this.Entity = new T();

                     this.SortValue = sortValue;
              }

              public string? SortValue { get; set; }

              public T Entity { get; set; }

              public static async Task<SortedPaginatedList<T>> CreateSortedCollectionAsync
              ( IQueryable<T> source, int pageIndex, int? pageSize, string? sortParam )
              {
                     int totalPages = await GetTotalPages( source, GetPageSize( pageSize ) );

                     int validPageIndex = GetPageIndex( totalPages, pageIndex );

                     List<T> items = await GetItemsList( source, validPageIndex, GetPageSize( pageSize ) );

                     return new SortedPaginatedList<T>( items, totalPages, validPageIndex, sortParam );
              }
       }
}
