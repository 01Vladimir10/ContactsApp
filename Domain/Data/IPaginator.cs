using System.Threading.Tasks;

namespace Domain.Data
{
    public interface IPaginator<T>
    {
        public int PageSize { get; set; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }

        public Page<T> CurrentPage { get; set; }
        public Task<IPaginator<T>> NextPage();
        public Task<IPaginator<T>> PreviousPage();
        public Task<IPaginator<T>> FetchFirstPage();
    }
}