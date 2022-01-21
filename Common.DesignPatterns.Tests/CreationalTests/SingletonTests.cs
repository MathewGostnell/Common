namespace Common.DesignPatterns.Tests.CreationalTests
{
    using Common.DesignPatterns.Creational.Singleton;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void GetInstance_SetValues_RetainsBetweenInstances()
        {
            var instance = Singleton<TestInstance>.GetInstance();
            instance.Name = "set value";
            var newInstance = Singleton<TestInstance>.GetInstance();

            newInstance.Name = "new set value";

            newInstance.Name.ShouldBe(instance.Name);
        }

        [TestMethod]
        public void GetInstance_SetValues_ReturnsOnInstance()
        {
            var instance = Singleton<TestInstance>.GetInstance();
            instance.Name = "set value";

            var newInstance = Singleton<TestInstance>.GetInstance();

            newInstance.Name.ShouldBe(instance.Name);
        }

        [TestMethod]
        public void GetInstance_StaticCall_ReturnsSameInstance()
        {
            var instance = Singleton<TestInstance>.GetInstance();

            var isDefaultInstance = instance == Singleton<TestInstance>.GetInstance();

            isDefaultInstance.ShouldBeTrue();
        }

        [TestMethod]
        public void GetInstance_StaticCall_ReturnsNotNull()
        {
            var instance = Singleton<TestInstance>.GetInstance();

            instance.ShouldNotBeNull();
        }
    }

    public class TestInstance
    {
        public string? Name
        {
            get;
            set;
        }
    }
}
