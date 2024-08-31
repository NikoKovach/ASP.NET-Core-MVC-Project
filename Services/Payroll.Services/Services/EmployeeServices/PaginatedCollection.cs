using Microsoft.EntityFrameworkCore;

namespace Payroll.Services.Services.EmployeeServices
{
       public class PaginatedCollection<T>
       {
              public PaginatedCollection( List<T> items, int count, int pageIndex )
              {
                     this.PageIndex = pageIndex;

                     this.TotalPages = count;

                     this.ItemsCollection.AddRange( items );
              }

              public int PageIndex { get; private set; }

              public int TotalPages { get; private set; }

              public bool HasPreviousPage => PageIndex > 1;

              public bool HasNextPage => PageIndex < TotalPages;

              public List<T> ItemsCollection { get; set; } = new List<T>();

              public static async Task<PaginatedCollection<T>> CreateCollectionAsync
              ( IQueryable<T> source, int pageIndex )
              {
                     int pageSize = 1;

                     var count = await source.CountAsync();

                     var items = await source.Skip( ( pageIndex - 1 ) * pageSize )
                                             .Take( pageSize )
                                             .ToListAsync();

                     return new PaginatedCollection<T>( items, count, pageIndex );
              }
       }
}