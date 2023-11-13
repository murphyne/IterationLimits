using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the number of iterations.
        /// </summary>
        /// <param name="enumerable">Original enumerable to be limited.</param>
        /// <param name="limit">Maximum number of iterations.</param>
        /// <inheritdoc cref="IEnumerable{T}"/>
        /// <returns>Limited enumerable.</returns>
        public static IEnumerable<T> LimitCount<T>(IEnumerable<T> enumerable, int limit)
        {
            var counter = 0;

            foreach (var element in enumerable)
            {
                if (counter >= limit) yield break;

                counter += 1;
                yield return element;
            }
        }
    }
}
