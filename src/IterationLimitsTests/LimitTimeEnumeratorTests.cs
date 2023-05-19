using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitTimeEnumeratorTests
    {
        private TimeSpan Elapsed => _now - _start;
        private DateTime _start = DateTime.Now;
        private DateTime _now = DateTime.Now;

        private static readonly TimeSpan Error = TimeSpan.FromMilliseconds(10);
        private static readonly TimeSpan Limited = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan Unlimited = TimeSpan.FromMilliseconds(600);

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
            IEnumerator<int> enumeratorUnlimited = GetEnumerator(Unlimited);

            while (enumeratorUnlimited.MoveNext())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Unlimited).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumeratorUnlimited = GetEnumerator(Unlimited);
            IEnumerator<int> enumeratorLimited = Limits.LimitTime(Limited, enumeratorUnlimited);

            while (enumeratorLimited.MoveNext())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Limited).Within(Error));
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
