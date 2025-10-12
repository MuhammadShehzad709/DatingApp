using Microsoft.EntityFrameworkCore;

namespace DatingApi.ApiHelpers
{
    public class PaginationReult<T>
    {
        public PaginationMetaData MetaData { get; set; } = default!;
        public List<T> Items { get; set; } = [];
    }
    public class PaginationMetaData
    {
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }

    }
    public class PaginationHelper()
    {
        public static async Task<PaginationReult<T>> CreateAsync<T>(IQueryable<T> query,int pageNumber,int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationReult<T>
            {
                MetaData = new PaginationMetaData
                {
                    currentPage = pageNumber,
                    totalPages = (int)Math.Ceiling(count / (double)pageSize),
                    pageSize = pageSize,
                    totalCount = count
                },
                Items = items
            };
        }
    }
}
