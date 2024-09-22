using Microsoft.EntityFrameworkCore;

namespace GameShop.Core;

public class PaginationList<T> : List<T>
{
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public PaginationList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count/ (double)pageSize);
        AddRange(items);
    }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static async Task<PaginationList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new(items, count, pageIndex, pageSize);
    }
}
