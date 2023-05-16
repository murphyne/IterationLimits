using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class EnumerableCountTests
    {
        private int _counter = 0;
        private const int Limited = 1_000;
        private const int Unlimited = 1_000_000;

        private IEnumerable<int> _enumerable;

        [SetUp]
        public void BeforeEach()
        {
            _counter = 0;
            _enumerable = GetNumbers();
        }

        [TearDown]
        public void AfterEach()
        {
            _enumerable = null;
        }

        [Test]
        public void TestSetup()
        {
            Assert.AreEqual(0, _counter);
        }

        [Test]
        public void TestUnlimited()
        {
            IEnumerable<int> enumerableUnlimited = _enumerable;

            foreach (var _ in enumerableUnlimited)
            {
                _counter += 1;
            }

            Assert.AreEqual(Unlimited, _counter);
        }

        [Test]
        public void TestLimited()
        {
            IEnumerable<int> enumerableUnlimited = _enumerable;
            IEnumerable<int> enumerableLimited = Limits.LimitCount(Limited, enumerableUnlimited);

            foreach (var _ in enumerableLimited)
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
