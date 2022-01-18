namespace Common.ExtensionLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System;

    [TestClass]
    public class IConvertibleExtensionsTests
    {
        [TestMethod]
        public void As_IntegerValue_ConvertsToString()
        {
            int? source = 5;
            string defaultValue = "1";

            var result = source.As(defaultValue);

            result.ShouldBe("5");
        }

        [TestMethod]
        public void As_NullValue_ReturnsDefaultValue()
        {
            string? source = null;
            int defaultValue = 1;

            var result = source.As(defaultValue);

            result.ShouldBe(defaultValue);
        }

        [TestMethod]
        public void As_StringValue_ConvertsToInteger()
        {
            string? source = "5";
            int defaultValue = 1;

            var result = source.As(defaultValue);

            result.ShouldBe(5);
        }
    }
}
