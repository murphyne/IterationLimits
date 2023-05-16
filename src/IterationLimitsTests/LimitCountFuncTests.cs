using System;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitCountFuncTests
    {
        private int _counter = 0;
        private const int Limited = 1_000;
        private const int Unlimited = 1_000_000;

        [SetUp]
        public void BeforeEach()
        {
            _counter = 0;
        }

        [Test]
        public void TestSetup()
        {
            Assert.AreEqual(0, _counter);
        }

        [Test]
        public void TestUnlimited()
        {
            Func<bool> conditionUnlimited = () => _counter < Unlimited;

            while (conditionUnlimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Unlimited, _counter);
        }

        [Test]
        public void TestLimited()
        {
            Func<bool> conditionUnlimited = () => _counter < Unlimited;
            Func<bool> conditionLimited = Limits.LimitCount(Limited, conditionUnlimited);

            while (conditionLimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Limited, _counter);
        }
    }
}
