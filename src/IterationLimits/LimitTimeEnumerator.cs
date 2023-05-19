﻿using System;
using System.Collections.Generic;

namespace IterationLimits
{
    public static partial class Limits
    {
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
