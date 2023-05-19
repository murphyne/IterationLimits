using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitTimeEnumerableTests
    {
        private TimeSpan Elapsed => _now - _start;
        private DateTime _start = DateTime.Now;
        private DateTime _now = DateTime.Now;

        private static readonly TimeSpan Error = TimeSpan.FromMilliseconds(10);
        private static readonly TimeSpan Short = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan Long = TimeSpan.FromMilliseconds(600);

        [SetUp]
        public void BeforeEach()
        {
            _start = DateTime.Now;
            _now = DateTime.Now;
        }

        [Test]
        public void TestSetup()
        {
            Assert.That(Elapsed, Is.EqualTo(TimeSpan.Zero).Within(Error));
        }

        [Test]
        public void TestUnlimited()
        {
            IEnumerable<int> enumerableUnlimited = GetEnumerable(Long);

            foreach (var _ in enumerableUnlimited)
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Long).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            IEnumerable<int> enumerableUnlimited = GetEnumerable(Long);
            IEnumerable<int> enumerableLimited = Limits.LimitTime(Short, enumerableUnlimited);

            foreach (var _ in enumerableLimited)
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Short).Within(Error));
        }

        [Test]
        public void TestUnlimitedShorterThanLimited()
        {
            IEnumerable<int> enumerableUnlimited = GetEnumerable(Short);
            IEnumerable<int> enumerableLimited = Limits.LimitTime(Long, enumerableUnlimited);

            foreach (var _ in enumerableLimited)
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Short).Within(Error));
        }

        private IEnumerable<int> GetEnumerable(TimeSpan limit)
        {
            var i = 0;
            while (Elapsed < limit)
            {
                yield return i++;
            }
        }
    }
}
