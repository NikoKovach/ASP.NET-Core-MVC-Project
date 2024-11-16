namespace Payroll.ViewModels.PagingViewModels
{
       public class PagingListForContracts<T, TFilter> : PagingListSortedFiltered<T, TFilter>
              where T : new()
              where TFilter : new()
       {
              public PagingListForContracts( List<T> items, int maxTotalPagest, int pageIndex,
                                                                       int pageSize, string? sortValue, TFilter? filter )
                     : base( items, maxTotalPagest, pageIndex, pageSize, sortValue, filter )
              {
              }

              public int? CompanyId { get; set; }

              public static async Task<PagingListForContracts<T, TFilter>> CreateAgreementsCollectionAsync
              ( IQueryable<T> source, int pageIndex, int? pageSize, string? sortParam, TFilter? filter )
              {
                     int totalPages = await GetTotalPages( source, pageSize ?? 1 );

                     int validPageIndex = GetPageIndex( totalPages, pageIndex );

                     List<T> items = await GetItemsList( source, validPageIndex, pageSize ?? 1 );

                     return new PagingListForContracts<T, TFilter>( items, totalPages, validPageIndex,
                                                                                                                pageSize ?? 1, sortParam, filter );
              }
       }
}
