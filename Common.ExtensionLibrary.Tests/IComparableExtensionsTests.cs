namespace Common.ExtensionLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System;

    [TestClass]
    public class IComparableExtensionsTests
    {
        [DataRow(1.0d, 0.0d, true)]
        [DataRow(0.0d, 0.0d, false)]
        [DataRow(0.0d, 1.0d, false)]
        [TestMethod]
        public void IsGreaterThan(
            double source,
            double target,
            bool expectedResult)
        {
            var result = source.IsGreaterThan(target);

            result.ShouldBe(expectedResult);
        }

        [DataRow(1.0d, 0.0d, true)]
        [DataRow(0.0d, 0.0d, true)]
        [DataRow(0.0d, 1.0d, false)]
        [TestMethod]
        public void IsGreaterThanOrEqualTo(
            double source,
            double target,
            bool expectedResult)
        {
            var result = source.IsGreaterThanOrEqualTo(target);

            result.ShouldBe(expectedResult);
        }

        [DataRow(1.0d, 0.0d, false)]
        [DataRow(0.0d, 0.0d, false)]
        [DataRow(0.0d, 1.0d, true)]
        [TestMethod]
        public void IsLessThan(
            double source,
            double target,
            bool expectedResult)
        {
            var result = source.IsLessThan(target);

            result.ShouldBe(expectedResult);
        }

        [DataRow(1.0d, 0.0d, false)]
        [DataRow(0.0d, 0.0d, true)]
        [DataRow(0.0d, 1.0d, true)]
        [TestMethod]
        public void IsLessThanOrEqualTo(
            double source,
            double target,
            bool expectedResult)
        {
            var result = source.IsLessThanOrEqualTo(target);

            result.ShouldBe(expectedResult);
        }
    }
}
