using System.Threading.Tasks;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;

namespace Database.Services
{
    public static class FaunaDbExtensions
    {
        public static async Task<T> ExecuteQuery<T>(this FaunaClient db, Expr expression, string path = "")
        {
            var data = await db.Query(expression);
            // decode and return the results
            return string.IsNullOrEmpty(path) ? Decoder.Decode<T>(data) : Decoder.Decode<T>(data.At(path));
        } 
    }
}