using System;

namespace IterationLimits
{
    public static class Limits
    {
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
    }
}
