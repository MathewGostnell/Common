namespace Common.ExtensionLibrary.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class GenericExtensionsTests
{
    [TestMethod]
    public void ArithmeticMean_IntegerList_ReturnsMeanBar()
    {
        IEnumerable<int>? intList = Enumerable.Range(1, 3);

        int arithmeticMean = intList.GetArithmeticMean();

        arithmeticMean.ShouldBe(2);
    }

    [TestMethod]
    public void GeometricMean_IntegerList_ReturnsMeanBar()
    {
        IEnumerable<double>? doubleList = new List<double>
        {
            1,
            2,
            3,
            4
        };
        double expectedResult = Math.Pow(
            doubleList.Aggregate(
                (current, next)
                => current *= next),
            1.0d / doubleList.Count());

        double arithmeticMean = doubleList.GetGeometricMean();

        arithmeticMean.ShouldBe(expectedResult);
    }
}