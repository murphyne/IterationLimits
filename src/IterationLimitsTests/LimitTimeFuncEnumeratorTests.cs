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
            IEnumerator<int> enumerator = GetEnumerator();

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();

            while (conditionUnlimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Unlimited).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumerator = GetEnumerator();

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();
            Func<bool> conditionLimited = Limits.LimitTime(Limited, conditionUnlimited);

            while (conditionLimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Limited).Within(Error));
        }

        private IEnumerator<int> GetEnumerator()
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
