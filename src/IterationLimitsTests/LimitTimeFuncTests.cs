using System;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitTimeFuncTests
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
            Func<bool> conditionUnlimited = () => Elapsed < Long;

            while (conditionUnlimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Long).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            Func<bool> conditionUnlimited = () => Elapsed < Long;
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
            Func<bool> conditionUnlimited = () => Elapsed < Short;
            Func<bool> conditionLimited = Limits.LimitTime(conditionUnlimited, Long);

            while (conditionLimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Short).Within(Error));
        }
    }
}
