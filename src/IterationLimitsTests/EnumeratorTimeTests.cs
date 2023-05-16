using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class EnumeratorTimeTests
    {
        private TimeSpan Elapsed => _now - _start;
        private DateTime _start = DateTime.Now;
        private DateTime _now = DateTime.Now;

        private static readonly TimeSpan Error = TimeSpan.FromMilliseconds(10);
        private static readonly TimeSpan Limited = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan Unlimited = TimeSpan.FromMilliseconds(600);

        private IEnumerator<int> _enumerator;

        [SetUp]
        public void BeforeEach()
        {
            _start = DateTime.Now;
            _now = DateTime.Now;
            _enumerator = GetNumbers();
        }

        [TearDown]
        public void AfterEach()
        {
            _enumerator = null;
        }

        [Test]
        public void TestSetup()
        {
            Assert.That(Elapsed, Is.EqualTo(TimeSpan.Zero).Within(Error));
        }

        [Test]
        public void TestUnlimited()
        {
            IEnumerator<int> enumeratorUnlimited = _enumerator;

            while (enumeratorUnlimited.MoveNext())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Unlimited).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumeratorUnlimited = _enumerator;
            IEnumerator<int> enumeratorLimited = Limits.LimitTime(Limited, enumeratorUnlimited);

            while (enumeratorLimited.MoveNext())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Limited).Within(Error));
        }

        private IEnumerator<int> GetNumbers()
        {
            var i = 0;
            yield return i;

            while (Elapsed < Unlimited)
            {
                i += 1;
                yield return i;
            }
        }
    }
}
