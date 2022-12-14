using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class FuncEnumeratorCountTests
    {
        private int _counter = 0;
        private const int Limited = 1_000;
        private const int Unlimited = 1_000_000;

        private IEnumerator<int> _iterator;

        [SetUp]
        public void BeforeEach()
        {
            _counter = 0;
            _iterator = GetNumbers().GetEnumerator();
        }

        [TearDown]
        public void AfterEach()
        {
            _iterator = null;
        }

        [Test]
        public void TestSetup()
        {
            Assert.AreEqual(0, _counter);
        }

        [Test]
        public void TestUnlimited()
        {
            Func<bool> conditionUnlimited = () => _iterator.MoveNext();

            while (conditionUnlimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Unlimited, _counter);
        }

        [Test]
        public void TestLimited()
        {
            Func<bool> conditionUnlimited = () => _iterator.MoveNext();
            Func<bool> conditionLimited = Limits.LimitCount(Limited, conditionUnlimited);

            while (conditionLimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Limited, _counter);
        }

        private IEnumerable<int> GetNumbers()
        {
            var i = 0;
            yield return i;

            while (_counter < Unlimited)
            {
                i += 1;
                yield return i;
            }
        }
    }
}
