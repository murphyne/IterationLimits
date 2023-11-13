using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the number of iterations.
        /// </summary>
        /// <param name="limit">Maximum number of iterations.</param>
        /// <param name="enumerator">Original enumerator to be limited.</param>
        /// <inheritdoc cref="IEnumerator{T}"/>
        /// <returns>Limited enumerator.</returns>
        public static IEnumerator<T> LimitCount<T>(int limit, IEnumerator<T> enumerator)
        {
            var counter = 0;

            return Limited();

            IEnumerator<T> Limited()
            {
                while (enumerator.MoveNext())
                {
                    if (counter >= limit) yield break;

                    counter += 1;
                    yield return enumerator.Current;
                }
            }
        }
    }
}
