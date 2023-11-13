using System;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitCountFuncTests
    {
        private int _counter = 0;
        private const int Short = 1_000;
        private const int Long = 1_000_000;

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
            Func<bool> conditionUnlimited = () => _counter < Long;

            while (conditionUnlimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Long, _counter);
        }

        [Test]
        public void TestLimited()
        {
            Func<bool> conditionUnlimited = () => _counter < Long;
            Func<bool> conditionLimited = Limits.LimitCount(conditionUnlimited, Short);

            while (conditionLimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Short, _counter);
        }

        [Test]
        public void TestUnlimitedShorterThanLimited()
        {
            Func<bool> conditionUnlimited = () => _counter < Short;
            Func<bool> conditionLimited = Limits.LimitCount(conditionUnlimited, Long);

            while (conditionLimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Short, _counter);
        }
    }
}
