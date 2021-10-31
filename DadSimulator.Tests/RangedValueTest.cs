using DadSimulator.Misc;
using NUnit.Framework;
using System;

namespace DadSimulator.Tests
{
    class RangedValueTest
    {
        [TestCase(10, 20, 15)]
        [TestCase(10, 20, 10)]
        [TestCase(10, 20, 20)]
        public void CreateRangedValue(double min, double max, double value)
        {
            Assert.DoesNotThrow(() => new RangedValue(min, max, value));
        }

        [TestCase(0, 10, 15)]
        [TestCase(0, 10, -1)]
        public void CreateValueOutsideRange(double min, double max, double value)
        {
            Assert.Throws<ArgumentException>(() => new RangedValue(min, max, value));
        }

        [TestCase(0, -10, -2)]
        [TestCase(100, 10, -2)]
        public void CreateImplausibleRange(double min, double max, double value)
        {
            Assert.Throws<ArgumentException>(() => new RangedValue(min, max, value));
        }

        [Test]
        public void ChangeValue()
        {
            const double min = 0, max = 100;
            var rv = new RangedValue(min, max, min);
            Assert.AreEqual(expected: min, rv.Value);

            rv.Change(-100);
            Assert.AreEqual(expected: min, rv.Value);

            rv.Change(max / 2);
            Assert.Greater(rv.Value, min);
            Assert.Less(rv.Value, max);

            rv.Change(max);
            Assert.AreEqual(expected: max, rv.Value);

            rv.Change(-2 * max);
            Assert.AreEqual(expected: min, rv.Value);
        }
    }
}
