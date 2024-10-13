//namespace Payroll.Services.Services
//{
//       public class PagingListSortedFiltered<T, TFilter> : PagingBaseList<T>
//              where T : new()
//              where TFilter : new()
//       {
//              public PagingListSortedFiltered( List<T> items, int maxTotalPagest, int pageIndex,
//                                                                                           string? sortValue, TFilter? filter ) :
//                     base( items, maxTotalPagest, pageIndex )
//              {
//                     this.Entity = new T();

//                     this.FilterVM = filter;

//                     this.SortValue = sortValue;
//              }

//              public string? SortValue { get; set; }

//              public TFilter FilterVM { get; set; }

//              public T Entity { get; set; }

//              public static async Task<PagingListSortedFiltered<T, TFilter>> CreateSortedCollectionAsync
//              ( IQueryable<T> source, int pageIndex, int? pageSize, string? sortParam, TFilter? filter )
//              {
//                     int totalPages = await GetTotalPages( source, GetPageSize( pageSize ) );

//                     int validPageIndex = GetPageIndex( totalPages, pageIndex );

//                     List<T> items = await GetItemsList( source, validPageIndex, GetPageSize( pageSize ) );

//                     return new PagingListSortedFiltered<T, TFilter>( items, totalPages, validPageIndex, sortParam, filter );
//              }
//       }
//}
