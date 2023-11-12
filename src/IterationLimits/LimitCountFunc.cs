using System;

namespace IterationLimits
{
    public static partial class Limits
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

            bool Limited()
            {
                if (counter >= limit) return false;

                counter += 1;
                return condition?.Invoke() ?? false;
            }

            return Limited;
        }
    }
}
