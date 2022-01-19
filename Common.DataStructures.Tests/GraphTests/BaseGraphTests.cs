namespace Common.DataStructures.Tests.GraphTests
{
    using Common.DataStructures.Graph;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System;

    [TestClass]
    public class BaseGraphTests
    {
        [TestMethod]
        public void AddEdge_ExistingEdge_ShouldReturnFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);
            baseGraph.AddEdge(1, 2);

            var result = baseGraph.AddEdge(1, 2);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void AddEdge_MissingVertex_ShouldThrowArgumentOutOfRange()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            Should.Throw<ArgumentOutOfRangeException>(
                    () => baseGraph.AddEdge(1, 2))
                .Message
                .ShouldContain("2");
        }

        [TestMethod]
        public void AddEdge_NewEdge_ShouldReturnTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);

            var result = baseGraph.AddEdge(1, 2);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_ReverseVertices_ShouldReturnTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);

            var result = baseGraph.AddEdge(1, 2)
                && baseGraph.AddEdge(2, 1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_SingleVertexEdge_ShouldReturnTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            var result = baseGraph.AddEdge(1, 1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddVertex_ExistingKey_ShouldReturnFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            var result = baseGraph.AddVertex(1);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void AddVertex_NewVertex_ShouldReturnTrue()
        {
            var baseGraph = new BaseGraph<int, string>();

            var result = baseGraph.AddVertex(1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void GetNeighbors_HasNoNeighbors_ReturnsEmptyList()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            var result = baseGraph.GetNeighbors(1);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetNeighbors_DefinedEdges_ReturnsListOfNeighbors()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);
            baseGraph.AddVertex(3);
            baseGraph.AddVertex(4);
            baseGraph.AddEdge(1, 2);
            baseGraph.AddEdge(2, 3);
            baseGraph.AddEdge(1, 3);
            baseGraph.AddEdge(3, 4);
            baseGraph.AddEdge(4, 1);

            var result = baseGraph.GetNeighbors(1);

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
        }

        [TestMethod]
        public void GetNeighbors_MissingVertex_ShouldThrowArgumentOutOfRange()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertex = 1;

            Should.Throw<ArgumentOutOfRangeException>(
                    () =>
                    baseGraph.GetNeighbors(missingVertex))
                .Message
                .ShouldContain($"{missingVertex}");
        }

        [TestMethod]
        public void GetVertexValue_MissingVertex_ReturnsDefaultValue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertexKey = 1;

            var result = baseGraph.GetVertexValue(missingVertexKey);

            result.ShouldBe(default(string));
        }

        [TestMethod]
        public void GetVertexValue_NeverSetValue_ReturnsDefaultValue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var vertexKey = 1;
            baseGraph.AddVertex(vertexKey);

            var result = baseGraph.GetVertexValue(vertexKey);

            result.ShouldBe(default(string));
        }

        [TestMethod]
        public void GetVertexValue_SetValue_ReturnsSetValue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var vertexKey = 1;
            var vertexValue = "test";
            baseGraph.AddVertex(vertexKey);
            baseGraph.SetVertexValue(
                vertexKey,
                vertexValue);

            var result = baseGraph.GetVertexValue(vertexKey);

            result.ShouldBe(vertexValue);
        }
    }
}
