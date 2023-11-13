﻿using System;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="condition">Original delegate to be limited.</param>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <returns>Limited delegate.</returns>
        public static Func<bool> LimitTime(Func<bool> condition, TimeSpan limit)
        {
            var start = DateTime.Now;

            return Limited;

            bool Limited()
            {
                var now = DateTime.Now;
                var elapsed = now - start;

                if (elapsed >= limit) return false;

                return condition?.Invoke() ?? false;
            }
        }
    }
}
