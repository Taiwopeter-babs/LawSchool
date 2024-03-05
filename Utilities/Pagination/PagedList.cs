namespace LawSchool.Utilities;

public class PagedList<T> : List<T>
{
    public PageMetaData PageMetaData { get; set; }

    public PagedList(List<T> items, int itemsCount, int pageNumber, int pageSize)
    {
        PageMetaData = new()
        {
            TotalItems = itemsCount,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(itemsCount / (double)pageSize)
        };

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source, int itemsCount,
        int pageNumber, int pageSize)
    {
        var items = source.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        return new PagedList<T>(items, itemsCount, pageNumber, pageSize);
    }
}