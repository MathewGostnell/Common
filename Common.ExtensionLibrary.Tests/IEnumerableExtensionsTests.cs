namespace Common.ExtensionLibrary.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;

[TestClass]
public class IEnumerableExtensionsTests
{
    [TestMethod]
    public void IsEmpty_EmptyList_ReturnsTrue()
    {
        var list = new List<int>();

        bool isEmpty = list.IsEmpty();

        isEmpty.ShouldBeTrue();
    }

    [TestMethod]
    public void IsEmpty_NullList_ThrowsArgumentNullRefernce()
    {
        IEnumerable<int> list = null;

        Should.Throw<ArgumentNullException>(
            () => list.IsEmpty());
    }

    [TestMethod]
    public void IsEmpty_PopulatedList_ReturnsFalse()
    {
        var list = new List<int>
            {
                5
            };

        bool isEmpty = list.IsEmpty();

        isEmpty.ShouldBeFalse();
    }

    [TestMethod]
    public void IsNullOrEmpty_EmptyList_ReturnsTrue()
    {
        var list = new List<int>();

        bool isNull = list.IsNullOrEmpty();

        isNull.ShouldBeTrue();
    }

    [TestMethod]
    public void IsNullOrEmpty_NullList_ReturnsTrue()
    {
        IList<int>? list = null;

        bool isNull = list.IsNullOrEmpty();

        isNull.ShouldBeTrue();
    }

    [TestMethod]
    public void IsNullOrEmpty_PopulatedList_ReturnsFalse()
    {
        var list = new List<int>
            {
                5
            };

        bool isNull = list.IsNullOrEmpty();

        isNull.ShouldBeFalse();
    }
}