using System;

namespace IterationLimits
{
    public static partial class Limits
    {
        /// <summary>
        /// Limits the number of iterations.
        /// </summary>
        /// <param name="condition">Original delegate to be limited.</param>
        /// <param name="limit">Maximum number of iterations.</param>
        /// <returns>Limited delegate.</returns>
        public static Func<bool> LimitCount(this Func<bool> condition, int limit)
        {
            var counter = 0;

            return Limited;

            bool Limited()
            {
                if (counter >= limit) return false;

                counter += 1;
                return condition?.Invoke() ?? false;
            }
        }
    }
}
