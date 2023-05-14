using System;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class FuncTimeTests
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
            Func<bool> conditionUnlimited = () => Elapsed < Unlimited;

            while (conditionUnlimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Unlimited).Within(Error));
        }

        [Test]
        public void TestLimited()
        {
            Func<bool> conditionUnlimited = () => Elapsed < Unlimited;
            Func<bool> conditionLimited = Limits.LimitTime(Limited, conditionUnlimited);

            while (conditionLimited.Invoke())
            {
                _now = DateTime.Now;
            }

            Assert.That(Elapsed, Is.EqualTo(Limited).Within(Error));
        }
    }
}
