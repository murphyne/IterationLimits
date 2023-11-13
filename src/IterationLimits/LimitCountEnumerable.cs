using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the number of iterations.
        /// </summary>
        /// <param name="limit">Maximum number of iterations.</param>
        /// <param name="enumerable">Original enumerable to be limited.</param>
        /// <inheritdoc cref="IEnumerable{T}"/>
        /// <returns>Limited enumerable.</returns>
        public static IEnumerable<T> LimitCount<T>(int limit, IEnumerable<T> enumerable)
        {
            var counter = 0;

            return Limited();

            IEnumerable<T> Limited()
            {
                foreach (var element in enumerable)
                {
                    if (counter >= limit) yield break;

                    counter += 1;
                    yield return element;
                }
            }
        }
    }
}
