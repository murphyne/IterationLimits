using System;
using System.Collections.Generic;

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
