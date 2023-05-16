using System;
using System.Collections.Generic;

namespace IterationLimits
{
    public static class Limits
    {
        /// <summary>
        /// Limits the number of iterations.
        /// </summary>
        /// <param name="limit">Maximum number of iterations.</param>
        /// <param name="condition">Original delegate to be limited.</param>
        /// <returns>Limited delegate.</returns>
        public static Func<bool> LimitCount(int limit, Func<bool> condition)
        {
            var counter = 0;

            return delegate
            {
                if (counter >= limit) return false;

                counter += 1;
                return condition?.Invoke() ?? false;
            };
        }

        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="condition">Original delegate to be limited.</param>
        /// <returns>Limited delegate.</returns>
        public static Func<bool> LimitTime(TimeSpan limit, Func<bool> condition)
        {
            var start = DateTime.Now;

            return delegate
            {
                var now = DateTime.Now;
                var elapsed = now - start;

                if (elapsed >= limit) return false;

                return condition?.Invoke() ?? false;
            };
        }

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
                    enumerator.MoveNext();
                    yield return enumerator.Current;
                }
            }

            return Limited();
        }

        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="enumerator">Original enumerator to be limited.</param>
        /// <inheritdoc cref="IEnumerator{T}"/>
        /// <returns>Limited enumerator.</returns>
        public static IEnumerator<T> LimitTime<T>(TimeSpan limit, IEnumerator<T> enumerator)
        {
            var start = DateTime.Now;

            IEnumerator<T> Limited()
            {
                while (true)
                {
                    var now = DateTime.Now;
                    var elapsed = now - start;

                    if (elapsed >= limit) yield break;

                    enumerator.MoveNext();
                    yield return enumerator.Current;
                }
            }

            return Limited();
        }

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

            IEnumerable<T> Limited()
            {
                foreach (var element in enumerable)
                {
                    if (counter >= limit) yield break;

                    counter += 1;
                    yield return element;
                }
            }

            return Limited();
        }

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

            IEnumerable<T> Limited()
            {
                foreach (var element in enumerable)
                {
                    var now = DateTime.Now;
                    var elapsed = now - start;

                    if (elapsed >= limit) yield break;

                    yield return element;
                }
            }

            return Limited();
        }
    }
}
