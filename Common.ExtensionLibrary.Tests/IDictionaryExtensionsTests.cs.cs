namespace Common.ExtensionLibrary.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;

[TestClass]
public class IDictionaryExtensionsTests
{
    [TestMethod]
    public void AddOrUpdate_ExistingEntry_ReturnsFalse()
    {
        string key = string.Empty;
        int value = 1;
        var dictionary = new Dictionary<string, int>
            {
                { key, value }
            };

        value = 5;
        bool result = dictionary.AddOrUpdate(
            key,
            value);

        result.ShouldBeFalse();
        dictionary[key].ShouldBe(value);
    }

    [TestMethod]
    public void AddOrUpdate_NewEntry_ReturnsTrue()
    {
        var dictionary = new Dictionary<string, int>();
        string key = string.Empty;
        int value = 1;

        bool result = dictionary.AddOrUpdate(
            key,
            value);

        result.ShouldBeTrue();
        dictionary[key].ShouldBe(value);
    }

    [TestMethod]
    public void AddOrUpdate_NewEntryWithoutValue_StoresDefaultValue()
    {
        var dictionary = new Dictionary<string, int>();
        string key = string.Empty;
        int value = default(int);

        bool result = dictionary.AddOrUpdate(key);

        result.ShouldBeTrue();
        dictionary[key].ShouldBe(value);
    }

    [TestMethod]
    public void AddOrUpdate_NullDictionary_ThrowsNullReferenceException()
    {
        IDictionary<string, int>? dictionary = null;

        // removed null reference warning as that is the scope of this test.
#pragma warning disable CS8604
        Should.Throw<NullReferenceException>(
            () =>
            dictionary.AddOrUpdate("throws exception", -1));
#pragma warning restore CS8604
    }
}