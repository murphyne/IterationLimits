using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitCountEnumeratorTests
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
            IEnumerator<int> enumeratorUnlimited = GetEnumerator(Long);

            while (enumeratorUnlimited.MoveNext())
            {
                _counter += 1;
            }

            Assert.AreEqual(Long, _counter);
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumeratorUnlimited = GetEnumerator(Long);
            IEnumerator<int> enumeratorLimited = Limits.LimitCount(Short, enumeratorUnlimited);

            while (enumeratorLimited.MoveNext())
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
