using System.Collections.Generic;
using System.Linq;

namespace Domain.Data
{
    public class Page<T>
    {
        public int PageSize { get; }
        public int PageNumber { get; }
        public IEnumerable<T> Items { get; }
        
        public int ItemsCount => Items.Count();

        public Page(IEnumerable<T> items, int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Items = items;
        }
    }
}