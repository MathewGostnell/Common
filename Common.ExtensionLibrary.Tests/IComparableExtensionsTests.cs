namespace Common.ExtensionLibrary.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

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
        bool isGreaterThan = source.IsGreaterThan(target);

        isGreaterThan.ShouldBe(expectedResult);
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
        bool isGreaterThanOrEqualTo = source.IsGreaterThanOrEqualTo(target);

        isGreaterThanOrEqualTo.ShouldBe(expectedResult);
    }

    [DataRow(0, false)]
    [DataRow(1, true)]
    [DataRow(-1, false)]
    [TestMethod]
    public void IsGreaterThanZero(
        int source,
        bool expectedResult)
    {
        bool isGreaterThanZero = source.IsGreaterThanZero();

        isGreaterThanZero.ShouldBe(expectedResult);
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
        bool isLessThan = source.IsLessThan(target);

        isLessThan.ShouldBe(expectedResult);
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
        bool isLessThanOrEqualTo = source.IsLessThanOrEqualTo(target);

        isLessThanOrEqualTo.ShouldBe(expectedResult);
    }

    [DataRow(0, false)]
    [DataRow(1, false)]
    [DataRow(-1, true)]
    [TestMethod]
    public void IsLessThanZero(
        int source,
        bool expectedResult)
    {
        bool isLessThanZero = source.IsLessThanZero();

        isLessThanZero.ShouldBe(expectedResult);
    }
}