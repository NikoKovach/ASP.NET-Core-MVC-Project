﻿//namespace Payroll.Services.Services
//{
//       public class zzzPagingListSortedFiltered<T, TFilter> : zzzPagingListSorted<T>
//              where T : new()
//              where TFilter : new()
//       {
//              public zzzPagingListSortedFiltered( List<T> items, int maxTotalPagest, int pageIndex, int pageSize,
//                                                                                           string? sortValue, TFilter? filter ) :
//                     base( items, maxTotalPagest, pageIndex, pageSize, sortValue )
//              {
//                     this.FilterVM = filter;
//              }

//              public TFilter FilterVM { get; set; }

//              public static async Task<zzzPagingListSortedFiltered<T, TFilter>> CreateSortedCollectionAsync
//              ( IQueryable<T> source, int pageIndex, int? pageSize, string? sortParam, TFilter? filter )
//              {
//                     int totalPages = await GetTotalPages( source, pageSize ?? 1 );

//                     int validPageIndex = GetPageIndex( totalPages, pageIndex );

//                     List<T> items = await GetItemsList( source, validPageIndex, pageSize ?? 1 );

//                     return new zzzPagingListSortedFiltered<T, TFilter>( items, totalPages, validPageIndex, pageSize ?? 1, sortParam, filter );
//              }
//       }
//}