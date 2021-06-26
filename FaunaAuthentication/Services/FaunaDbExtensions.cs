using System.Linq.Expressions;
using System.Threading.Tasks;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;

namespace FaunaAuthentication.Services
{
    public static class FaunaDbExtensions
    {
        public static async Task<T> ExecuteQuery<T>(this FaunaClient db, Expr expression, string path = "")
        {
            var data = await db.Query(expression);
            // decode and return the results
            return string.IsNullOrEmpty(path) ? Decoder.Decode<T>(data) : Decoder.Decode<T>(data.At(path));
        }

        public static T To<T>(this Value value, string path = "")
            => string.IsNullOrEmpty(path) ? Decoder.Decode<T>(value) : Decoder.Decode<T>(value.At(path));

        public static Value ToValue(this object input) => Encoder.Encode(input);
    }
}