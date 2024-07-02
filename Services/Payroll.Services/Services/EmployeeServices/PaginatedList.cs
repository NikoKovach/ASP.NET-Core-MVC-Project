﻿
using Microsoft.EntityFrameworkCore;

namespace Payroll.Services.Services.EmployeeServices
{
	public class PaginatedList<T> : List<T>
	{
		public PaginatedList(List<T> items, int count, int pageIndex)
		{
			PageIndex = pageIndex;

			TotalPages = count;

			this.AddRange(items);

		}

		//public string VirtualFilePath { get; set; }

		public int PageIndex { get; private set; }

		public int TotalPages { get; private set; }

		public bool HasPreviousPage => PageIndex > 1;

		public bool HasNextPage => PageIndex < TotalPages;
		
		public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, 
			int pageIndex)
		{
			int pageSize = 1;

			var count = await source.CountAsync();

			var items = await source.Skip( ( pageIndex - 1 ) * pageSize )
						   .Take( pageSize )
						   .ToListAsync();

			return new PaginatedList<T>(items, count, pageIndex);
		}
	}
}

//public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, 
		//	int pageIndex, int pageSize)
		//{
		//	var count = await source.CountAsync();
		//	var items = await source
		//				   .Skip((pageIndex - 1) * pageSize)
		//				   .Take(pageSize)
		//				   .ToListAsync();

		//	return new PaginatedList<T>(items, count, pageIndex, pageSize);
		//}
//TotalPages = (int)Math.Ceiling(count / (double)pageSize);