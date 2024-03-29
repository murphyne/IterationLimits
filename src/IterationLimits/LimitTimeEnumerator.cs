﻿using System;
using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="enumerator">Original enumerator to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <inheritdoc cref="IEnumerator{T}"/>
        /// <returns>Limited enumerator.</returns>
        public static IEnumerator<T> LimitTime<T>(this IEnumerator<T> enumerator, TimeSpan limit)
        {
            var start = DateTime.Now;

            while (enumerator.MoveNext())
            {
                var now = DateTime.Now;
                var elapsed = now - start;

                if (elapsed >= limit) yield break;

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="enumerator">Original enumerator to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="measure">A function to measure time.</param>
        /// <inheritdoc cref="IEnumerator{T}"/>
        /// <returns>Limited enumerator.</returns>
        public static IEnumerator<T> LimitTime<T>(this IEnumerator<T> enumerator, TimeSpan limit, Func<DateTime> measure)
        {
            var start = measure();

            while (enumerator.MoveNext())
            {
                var now = measure();
                var elapsed = now - start;

                if (elapsed >= limit) yield break;

                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="enumerator">Original enumerator to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="measure">A function to measure time.</param>
        /// <inheritdoc cref="IEnumerator{T}"/>
        /// <returns>Limited enumerator.</returns>
        public static IEnumerator<T> LimitTime<T>(this IEnumerator<T> enumerator, TimeSpan limit, Func<TimeSpan> measure)
        {
            var start = measure();

            while (enumerator.MoveNext())
            {
                var now = measure();
                var elapsed = now - start;

                if (elapsed >= limit) yield break;

                yield return enumerator.Current;
            }
        }
    }
}
