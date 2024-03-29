﻿using System;
using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="enumerable">Original enumerable to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <inheritdoc cref="IEnumerable{T}"/>
        /// <returns>Limited enumerable.</returns>
        public static IEnumerable<T> LimitTime<T>(this IEnumerable<T> enumerable, TimeSpan limit)
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

        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="enumerable">Original enumerable to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="measure">A function to measure time.</param>
        /// <inheritdoc cref="IEnumerable{T}"/>
        /// <returns>Limited enumerable.</returns>
        public static IEnumerable<T> LimitTime<T>(this IEnumerable<T> enumerable, TimeSpan limit, Func<DateTime> measure)
        {
            var start = measure();

            foreach (var element in enumerable)
            {
                var now = measure();
                var elapsed = now - start;

                if (elapsed >= limit) yield break;

                yield return element;
            }
        }

        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="enumerable">Original enumerable to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="measure">A function to measure time.</param>
        /// <inheritdoc cref="IEnumerable{T}"/>
        /// <returns>Limited enumerable.</returns>
        public static IEnumerable<T> LimitTime<T>(this IEnumerable<T> enumerable, TimeSpan limit, Func<TimeSpan> measure)
        {
            var start = measure();

            foreach (var element in enumerable)
            {
                var now = measure();
                var elapsed = now - start;

                if (elapsed >= limit) yield break;

                yield return element;
            }
        }
    }
}
