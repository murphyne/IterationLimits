using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class LimitCountEnumerableTests
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
            IEnumerable<int> enumerableUnlimited = GetEnumerable(Long);

            foreach (var _ in enumerableUnlimited)
            {
                _counter += 1;
            }

            Assert.AreEqual(Long, _counter);
        }

        [Test]
        public void TestLimited()
        {
            IEnumerable<int> enumerableUnlimited = GetEnumerable(Long);
            IEnumerable<int> enumerableLimited = Limits.LimitCount(Short, enumerableUnlimited);

            foreach (var _ in enumerableLimited)
            {
                _counter += 1;
            }

            Assert.AreEqual(Short, _counter);
        }

        [Test]
        public void TestUnlimitedShorterThanLimited()
        {
            IEnumerable<int> enumerableUnlimited = GetEnumerable(Short);
            IEnumerable<int> enumerableLimited = Limits.LimitCount(Long, enumerableUnlimited);

            foreach (var _ in enumerableLimited)
            {
                _counter += 1;
            }

            Assert.AreEqual(Short, _counter);
        }

        private IEnumerable<int> GetEnumerable(int limit)
        {
            var i = 0;
            while (_counter < limit)
            {
                yield return i++;
            }
        }
    }
}
