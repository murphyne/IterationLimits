using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class IteratorTimeTests
    {
        private TimeSpan Elapsed => _now - _start;
        private DateTime _start = DateTime.Now;
        private DateTime _now = DateTime.Now;

        private static readonly TimeSpan Error = TimeSpan.FromMilliseconds(10);
        private static readonly TimeSpan Limited = TimeSpan.FromMilliseconds(200);
        private static readonly TimeSpan Unlimited = TimeSpan.FromMilliseconds(600);

        private IEnumerator<int> _iterator;

        [SetUp]
        public void BeforeEach()
        {
            _start = DateTime.Now;
            _now = DateTime.Now;
            _iterator = GetNumbers().GetEnumerator();
        }

        [Test]
        public void TestSetup()
        {
            Assert.AreEqual(TimeSpan.Zero, Elapsed);
        }

        [Test]
        public void TestUnlimited()
        {
            IEnumerator<int> iteratorUnlimited = _iterator;

            while (iteratorUnlimited.MoveNext())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Unlimited).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> iteratorUnlimited = _iterator;
            IEnumerator<int> iteratorLimited = Limits.LimitTime(Limited, iteratorUnlimited);

            while (iteratorLimited.MoveNext())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Limited).Within(Error));
        }

        private IEnumerable<int> GetNumbers()
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
