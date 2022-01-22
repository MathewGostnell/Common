namespace Common.ExtensionLibrary.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
public class StringExtensionsTests
{
    [TestMethod]
    public void IsEqualTo_CaseSensitive_ReturnsFalse()
    {
        string source = " TeSt";
        string target = "tEsT ";

        bool result = source.IsEqualTo(
            target,
            isCaseSensitive: true);

        result.ShouldBeFalse();
    }

    [TestMethod]
    public void IsEqualTo_DefaultComparison_IgnoresCaseAndWhiteSpace()
    {
        string source = " TeSt";
        string target = "tEsT ";

        bool result = source.IsEqualTo(target);

        result.ShouldBeTrue();
    }

    [TestMethod]
    public void IsEqualTo_NullValues_ReturnTrue()
    {
        string? source = null;
        string? target = null;

        bool result = source.IsEqualTo(target);

        result.ShouldBeTrue();
    }

    [TestMethod]
    public void IsEqualTo_WhiteSpaceSensitive_ReturnsFalse()
    {
        string source = " TeSt";
        string target = "tEsT ";

        bool result = source.IsEqualTo(
            target,
            isWhiteSpaceSensitive: true);

        result.ShouldBeFalse();
    }

    [DataRow(null, true)]
    [DataRow("", true)]
    [DataRow(" ", false)]
    [DataRow("test", false)]
    [TestMethod]
    public void IsNullOrEmpty(
        string? value,
        bool expectedResult)
    {
        bool result = value.IsNullOrEmpty();

        result.ShouldBe(expectedResult);
    }

    [DataRow(null, true)]
    [DataRow("", true)]
    [DataRow(" ", true)]
    [DataRow("test", false)]
    [TestMethod]
    public void IsNullOrWhiteSpace(
        string? value,
        bool expectedResult)
    {
        bool result = value.IsNullOrWhiteSpace();

        result.ShouldBe(expectedResult);
    }

    [TestMethod]
    public void SafeTrim_NullValue_ReturnsDefaultValue()
    {
        string? value = null;

        string? result = value.SafeTrim("default");

        result.ShouldBe("default");
    }

    [TestMethod]
    public void SafeTrim_NullValueWithoutDefault_ReturnsEmptyString()
    {
        string? value = null;

        string? result = value.SafeTrim();

        result.ShouldBe(string.Empty);
    }
}