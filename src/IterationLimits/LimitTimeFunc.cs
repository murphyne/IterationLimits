using System;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the time duration of iterations.
        /// </summary>
        /// <param name="limit">Maximum time duration of iterations.</param>
        /// <param name="condition">Original delegate to be limited.</param>
        /// <returns>Limited delegate.</returns>
        public static Func<bool> LimitTime(TimeSpan limit, Func<bool> condition)
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
