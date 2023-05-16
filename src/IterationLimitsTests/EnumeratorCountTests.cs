using System.Collections.Generic;
using IterationLimits;
using NUnit.Framework;

namespace IterationLimitsTests
{
    [TestFixture]
    public class EnumeratorCountTests
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
            IEnumerator<int> enumeratorUnlimited = GetEnumerator();

            while (enumeratorUnlimited.MoveNext())
            {
                _counter += 1;
            }

            Assert.AreEqual(Unlimited, _counter);
        }

        [Test]
        public void TestLimited()
        {
            IEnumerator<int> enumeratorUnlimited = GetEnumerator();
            IEnumerator<int> enumeratorLimited = Limits.LimitCount(Limited, enumeratorUnlimited);

            while (enumeratorLimited.MoveNext())
            {
                _counter += 1;
            }

            Assert.AreEqual(Limited, _counter);
        }

        private IEnumerator<int> GetEnumerator()
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
