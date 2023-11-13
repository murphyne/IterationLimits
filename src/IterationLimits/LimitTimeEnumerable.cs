using System;
using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="enumerable">Original enumerable to be limited.</param>
        /// <inheritdoc cref="IEnumerable{T}"/>
        /// <returns>Limited enumerable.</returns>
        public static IEnumerable<T> LimitTime<T>(TimeSpan limit, IEnumerable<T> enumerable)
        {
            var start = DateTime.Now;

            foreach (var element in enumerable)
            {
                var now = DateTime.Now;
                var elapsed = now - start;

                if (elapsed >= limit) yield break;

                yield return element;
            }
        }
    }
}
