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

            IEnumerator<T> Limited()
            {
                while (true)
                {
                    if (counter >= limit) yield break;

                    counter += 1;
                    if (enumerator.MoveNext())
                        yield return enumerator.Current;
                    else
                        yield break;
                }
            }

            return Limited();
        }
    }
}
