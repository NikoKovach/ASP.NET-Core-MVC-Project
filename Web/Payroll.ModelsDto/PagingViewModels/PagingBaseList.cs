using Microsoft.EntityFrameworkCore;

namespace Payroll.ViewModels.PagingViewModels
{
       public class PagingBaseList<T>
       {
              public PagingBaseList( List<T> items, int maxTotalPagest, int pageIndex, int pageSize )
              {
                     this.PageIndex = pageIndex;

                     this.PageSize = pageSize;

                     this.TotalPages = maxTotalPagest;

                     this.ItemsCollection.AddRange( items );
              }

              public int PageIndex { get; private set; }

              public int PageSize { get; set; }

              public int TotalPages { get; private set; }

              public bool HasPreviousPage => PageIndex > 1;

              public bool HasNextPage => PageIndex < TotalPages;

              public List<T> ItemsCollection { get; set; } = new List<T>();

              public string? RouteEdit { get; set; }

              public static async Task<PagingBaseList<T>> CreateCollectionAsync
              ( IQueryable<T> source, int pageIndex, int? pageSize )
              {
                     int totalPages = await GetTotalPages( source, pageSize ?? 1 );

                     int validPageIndex = GetPageIndex( totalPages, pageIndex );

                     List<T> items = await GetItemsList( source, validPageIndex, pageSize ?? 1 );

                     return new PagingBaseList<T>( items, totalPages, validPageIndex, pageSize ?? 1 );
              }

              //#######################################################

              protected static int GetPageIndex( int totalPages, int pageIndex )
              {
                     int realPageIndex = ( pageIndex > totalPages ) ? totalPages : pageIndex;

                     if ( pageIndex < 1 || realPageIndex < 1 )
                            realPageIndex = 1;

                     return realPageIndex;
              }

              protected static async Task<int> GetTotalPages( IQueryable<T> source, int pageSize )
              {
                     int count = await source.CountAsync();

                     int maxTotalPages = (int) Math.Ceiling( count / (double) pageSize );

                     return maxTotalPages;
              }

              protected static async Task<List<T>> GetItemsList( IQueryable<T> source, int pageIndex, int pageSize )
              {
                     List<T> items = await source.Skip( ( pageIndex - 1 ) * pageSize )
                                                                       .Take( pageSize )
                                                                       .ToListAsync();

                     return items;
              }
       }
}

//int realPageSize = pageSize ?? 1;

//int count = await source.CountAsync();

//int maxTotalPages = (int) Math.Ceiling( count / (double) realPageSize );
//int realPageIndex = ( pageIndex > maxTotalPages ) ? maxTotalPages : pageIndex;

//if ( pageIndex < 1 || realPageIndex < 1 )
//       realPageIndex = 1;

//List<T> items = await source.Skip( ( realPageIndex - 1 ) * realPageSize )
//                                                  .Take( realPageSize )
//                                                  .ToListAsync();
