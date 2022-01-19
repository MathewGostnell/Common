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
        public void AddEdge_ExistingEdge_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);
            baseGraph.AddEdge(1, 2);

            var result = baseGraph.AddEdge(1, 2);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void AddEdge_MissingVertex_ThrowArgumentOutOfRange()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            Should.Throw<ArgumentOutOfRangeException>(
                    () => baseGraph.AddEdge(1, 2))
                .Message
                .ShouldContain("2");
        }

        [TestMethod]
        public void AddEdge_NewEdge_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);

            var result = baseGraph.AddEdge(1, 2);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_ReverseVertices_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);
            baseGraph.AddVertex(2);

            var result = baseGraph.AddEdge(1, 2)
                && baseGraph.AddEdge(2, 1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_SingleVertexEdge_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            var result = baseGraph.AddEdge(1, 1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddVertex_ExistingKey_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            baseGraph.AddVertex(1);

            var result = baseGraph.AddVertex(1);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void AddVertex_NewVertex_ReturnsTrue()
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
        public void GetNeighbors_MissingVertex_ReturnsEmptyList()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertex = 1;

            var result = baseGraph.GetNeighbors(missingVertex);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
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

        [TestMethod]
        public void IsAdjacent_ExistingEdge_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var sourceKey = 1;
            var targetKey = 2;
            baseGraph.AddVertex(sourceKey);
            baseGraph.AddVertex(targetKey);
            baseGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = baseGraph.IsAdjacent(
                sourceKey,
                targetKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void IsAdjacent_MissingVertex_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertexKey = 1;

            var result = baseGraph.IsAdjacent(
                missingVertexKey,
                missingVertexKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void IsAdjacent_ReversedVertices_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            var sourceKey = 1;
            var targetKey = 2;
            baseGraph.AddVertex(sourceKey);
            baseGraph.AddVertex(targetKey);
            baseGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = baseGraph.IsAdjacent(
                targetKey,
                sourceKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void IsAdjacent_SelfEdgeVertex_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var vertexKey = 1;
            baseGraph.AddVertex(vertexKey);
            baseGraph.AddEdge(
                vertexKey,
                vertexKey);

            var result = baseGraph.IsAdjacent(
                vertexKey,
                vertexKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveEdge_ExistingEdge_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var sourceKey = 1;
            var targetKey = 2;
            baseGraph.AddVertex(sourceKey);
            baseGraph.AddVertex(targetKey);
            baseGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = baseGraph.RemoveEdge(
                sourceKey,
                targetKey);

            result.ShouldBeTrue();
        }


        [TestMethod]
        public void RemoveEdge_MissingVertex_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertexKey = 1;
            
            var result = baseGraph.RemoveEdge(
                missingVertexKey,
                missingVertexKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_ReversedVertex_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            var sourceKey = 1;
            var targetKey = 2;
            baseGraph.AddVertex(sourceKey);
            baseGraph.AddVertex(targetKey);
            baseGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = baseGraph.RemoveEdge(
                targetKey,
                sourceKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_SelfEdgeVertex_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var vertexKey= 1;
            baseGraph.AddVertex(vertexKey);
            baseGraph.AddEdge(
                vertexKey,
                vertexKey);

            var result = baseGraph.RemoveEdge(
                vertexKey,
                vertexKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveVertex_ExistingVertex_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var vertexKey = 1;
            baseGraph.AddVertex(vertexKey);

            var result = baseGraph.RemoveVertex(vertexKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveVertex_MissingVertex_ReturnsFalse()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertexKey = 1;

            var result = baseGraph.RemoveVertex(missingVertexKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveVertex_ExistingEdges_ReturnsTrueAndRemovesEdges()
        {
            // TODO split into multiple RemoveVertex_ExistingEdges_... unit tests
            var baseGraph = new BaseGraph<int, string>();
            var sourceKey = 1;
            var targetKey = 2;
            var tertiaryKey = 3;
            baseGraph.AddVertex(sourceKey);
            baseGraph.AddVertex(targetKey);
            baseGraph.AddVertex(tertiaryKey);
            baseGraph.AddEdge(
                sourceKey, 
                targetKey);
            baseGraph.AddEdge(
                targetKey,
                tertiaryKey);

            var result = baseGraph.RemoveVertex(sourceKey);

            result.ShouldBeTrue();
            baseGraph
                .GetNeighbors(sourceKey)
                .ShouldBeEmpty();
            baseGraph.IsAdjacent(
                    sourceKey,
                    targetKey)
                .ShouldBeFalse();
            baseGraph.IsAdjacent(
                    targetKey,
                    tertiaryKey)
                .ShouldBeTrue();
            baseGraph
                .GetVertexValue(sourceKey)
                .ShouldBe(default(string));
        }

        [TestMethod]
        public void SetVertexValue_ExistingVertex_ReturnsTrue()
        {
            var baseGraph = new BaseGraph<int, string>();
            var vertexKey = 1;
            var vertexValue = "test";
            baseGraph.AddVertex(vertexKey);

            var result = baseGraph.SetVertexValue(
                vertexKey,
                vertexValue);

            result.ShouldBeTrue();
            baseGraph
                .GetVertexValue(vertexKey)
                .ShouldBe(vertexValue);
        }

        [TestMethod]
        public void SetVertexValue_MissingVertex_ThrowsArgumentOutOfRange()
        {
            var baseGraph = new BaseGraph<int, string>();
            var missingVertexKey = 1;

            Should.Throw<ArgumentOutOfRangeException>(
                    () =>
                    baseGraph.SetVertexValue(
                        missingVertexKey,
                        "test"))
                .Message
                .ShouldContain($"{missingVertexKey}");
        }
    }
}
