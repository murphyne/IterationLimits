using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitTimeFuncEnumeratorTests
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
            IEnumerator<int> enumerator = GetEnumerator(Long);

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();

            while (conditionUnlimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Long).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumerator = GetEnumerator(Long);

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();
            Func<bool> conditionLimited = Limits.LimitTime(conditionUnlimited, Short);

            while (conditionLimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Short).Within(Error));
        }

        [Test]
        public void TestUnlimitedShorterThanLimited()
        {
            IEnumerator<int> enumerator = GetEnumerator(Short);

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();
            Func<bool> conditionLimited = Limits.LimitTime(conditionUnlimited, Long);

            while (conditionLimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Short).Within(Error));
        }

        private IEnumerator<int> GetEnumerator(TimeSpan limit)
        {
            var i = 0;
            while (Elapsed < limit)
            {
                yield return i++;
            }
        }
    }
}
