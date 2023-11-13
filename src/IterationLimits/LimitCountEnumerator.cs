using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the number of iterations.
        /// </summary>
        /// <param name="enumerator">Original enumerator to be limited.</param>
        /// <param name="limit">Maximum number of iterations.</param>
        /// <inheritdoc cref="IEnumerator{T}"/>
        /// <returns>Limited enumerator.</returns>
        public static IEnumerator<T> LimitCount<T>(this IEnumerator<T> enumerator, int limit)
        {
            var counter = 0;

            while (enumerator.MoveNext())
            {
                if (counter >= limit) yield break;

                counter += 1;
                yield return enumerator.Current;
            }
        }
    }
}
