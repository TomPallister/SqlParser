using System.Collections.Generic;
using System.Linq;

namespace Fourth.SqlParser.Infrastructure.Infrastructure.Extensions
{
    public static class EnumerableExtension
    {
        public static IEnumerable<IList<T>> Chunk<T>(this IList<T> source, int chunksize)
        {
            int pos = 0;
            while (source.Skip(pos).Any())
            {
                yield return source.Skip(pos).Take(chunksize).ToList();
                pos += chunksize;
            }
        }
    }
}