using System;
using System.Threading.Tasks;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;

namespace Database.Utils
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

        public static Value Encode(this object input) => Encoder.Encode(input);

        public static T Decode<T>(this Value value, string path = "")
            => string.IsNullOrEmpty(path) ? Decoder.Decode<T>(value) : Decoder.Decode<T>(value.At(path));
        public static T ToOrDefault<T>(this Value value, string path = "")
        {
            try
            {
                return string.IsNullOrEmpty(path) ? Decoder.Decode<T>(value) : Decoder.Decode<T>(value.At(path));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}