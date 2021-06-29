using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Utils;
using Domain.Data;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;
using static FaunaDB.Query.Language;

namespace Database.Services
{
    public class FaunaPaginator<T> : IPaginator<T>
    {
        private readonly FaunaClient _db;
        private RefV _previousRef, _nextRef;
        private int pageCounter = 0;
        private readonly Expr _sourceExpr;
        public int PageSize { get; set; }
        public bool HasNextPage => _nextRef != null;
        public bool HasPreviousPage => _previousRef != null;
        public Page<T> CurrentPage { get; set; }
        private readonly Func<Value, IEnumerable<T>> _castFunc;
        private readonly Expr _getLambda;

        public FaunaPaginator(FaunaClient client, Expr sourceExpr, Func<Value, IEnumerable<T>> castFunc, int pageSize, Expr getLambda = null)
        {
            _db = client;
            _sourceExpr = sourceExpr;
            _castFunc = castFunc;
            PageSize = pageSize;
            _getLambda = getLambda ?? Lambda(x => Get(x));
        }

        public async Task<IPaginator<T>> NextPage()
        {
            if (!HasNextPage) return this;
            pageCounter++;
            await FetchPage(Paginate(_sourceExpr, size: PageSize, after: _nextRef));
            return this;
        }

        public async Task<IPaginator<T>> PreviousPage()
        {
            if (!HasPreviousPage) return this;
            pageCounter--;
            await FetchPage(Paginate(_sourceExpr, size: PageSize, before: _previousRef));
            return this;
        }

        public async Task<IPaginator<T>> FetchFirstPage()
        {
            pageCounter++;
            await FetchPage(Paginate(_sourceExpr, size: PageSize));
            return this;
        }

        private async Task<IPaginator<T>> FetchPage(Expr itemsArray)
        {
            // Console.WriteLine($"Next: {_nextRef}");
            // Console.WriteLine($"Query: {itemsArray}");
            //
            var page = await _db.Query(Map(itemsArray, _getLambda));
            _nextRef = GetCursorReference(page.At("after"));
            _previousRef = GetCursorReference(page.At("before"));
            var items = _castFunc(page.At("data"));
            CurrentPage = new Page<T>(items, PageSize, pageCounter);
            return this;
        }

        private static RefV GetCursorReference(Value value)
        {
            if (value == null) return default;
            try
            {
                var array = value.Decode<ArrayV>();
                return array[0].Decode<RefV>();
            }
            catch (Exception)
            {
                return default;
            }
        }
        
    }
}