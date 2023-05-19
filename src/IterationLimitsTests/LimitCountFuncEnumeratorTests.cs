using System;
using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitCountFuncEnumeratorTests
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
            IEnumerator<int> enumerator = GetEnumerator(Long);

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();

            while (conditionUnlimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Long, _counter);
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumerator = GetEnumerator(Long);

            Func<bool> conditionUnlimited = () => enumerator.MoveNext();
            Func<bool> conditionLimited = Limits.LimitCount(Short, conditionUnlimited);

            while (conditionLimited.Invoke())
            {
                _counter += 1;
            }

            Assert.AreEqual(Short, _counter);
        }

        private IEnumerator<int> GetEnumerator(int limit)
        {
            var i = 0;
            while (_counter < limit)
            {
                yield return i++;
            }
        }
    }
}
