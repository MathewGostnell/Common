namespace Common.DataStructures.Tests.GraphTests
{
    using Common.DataStructures.Graph;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System.Collections.Generic;

    [TestClass]
    public class WeightedGraphTests
    {
        [TestMethod]
        public void AddEdge_ExistingEdge_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);
            weightedGraph.AddVertex(2);
            weightedGraph.AddEdge(1, 2);

            var result = weightedGraph.AddEdge(1, 2);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void AddEdge_MissingVertex_ThrowKeyNotFound()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);

            Should.Throw<KeyNotFoundException>(
                    () => weightedGraph.AddEdge(1, 2))
                .Message
                .ShouldContain("2");
        }

        [TestMethod]
        public void AddEdge_NewEdge_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);
            weightedGraph.AddVertex(2);

            var result = weightedGraph.AddEdge(1, 2);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_ReverseVertices_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);
            weightedGraph.AddVertex(2);

            var result = weightedGraph.AddEdge(1, 2)
                && weightedGraph.AddEdge(2, 1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_SingleVertexEdge_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);

            var result = weightedGraph.AddEdge(1, 1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void AddVertex_ExistingKey_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);

            var result = weightedGraph.AddVertex(1);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void AddVertex_NewVertex_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;

            var result = weightedGraph.AddVertex(1);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void GetEdgeWeight_ExistingEdge_ReturnsSetWeight()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();
            var sourceKey = 1;
            var targetKey = 2;
            var weight = 3;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddEdge(
                sourceKey,
                targetKey);
            weightedGraph.SetEdgeWeight(
                sourceKey,
                targetKey,
                weight);

            var edgeWeight = weightedGraph.GetEdgeWeight(
                sourceKey,
                targetKey);

            edgeWeight.ShouldBe(weight);
        }

        [TestMethod]
        public void GetEdgeWeight_MissingEdge_ReturnsDefaultWeight()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();
            var missingSourceKey = 1;
            var missingTargetKey = 2;

            var edgeWeight = weightedGraph.GetEdgeWeight(
                missingSourceKey,
                missingTargetKey);

            edgeWeight.ShouldBe(default(int));
        }

        [TestMethod]
        public void GetEdgeWeight_ReversedEdge_ReturnsDefaultWeight()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();
            var sourceKey = 1;
            var targetKey = 2;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddEdge(
                sourceKey,
                targetKey);

            var edgeWeight = weightedGraph.GetEdgeWeight(
                targetKey,
                sourceKey);

            edgeWeight.ShouldBe(default(int));
        }

        [TestMethod]
        public void GetNeighbors_HasNoNeighbors_ReturnsEmptyList()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);

            var result = weightedGraph.GetNeighbors(1);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetNeighbors_DefinedEdges_ReturnsListOfNeighbors()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            weightedGraph.AddVertex(1);
            weightedGraph.AddVertex(2);
            weightedGraph.AddVertex(3);
            weightedGraph.AddVertex(4);
            weightedGraph.AddEdge(1, 2);
            weightedGraph.AddEdge(2, 3);
            weightedGraph.AddEdge(1, 3);
            weightedGraph.AddEdge(3, 4);
            weightedGraph.AddEdge(4, 1);

            var result = weightedGraph.GetNeighbors(1);

            result.ShouldNotBeNull();
            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
        }

        [TestMethod]
        public void GetNeighbors_MissingVertex_ReturnsEmptyList()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var missingVertex = 1;

            var result = weightedGraph.GetNeighbors(missingVertex);

            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetVertexValue_MissingVertex_ReturnsDefaultValue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var missingVertexKey = 1;

            var result = weightedGraph.GetVertexValue(missingVertexKey);

            result.ShouldBe(default(string));
        }

        [TestMethod]
        public void GetVertexValue_NeverSetValue_ReturnsDefaultValue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var vertexKey = 1;
            weightedGraph.AddVertex(vertexKey);

            var result = weightedGraph.GetVertexValue(vertexKey);

            result.ShouldBe(default(string));
        }

        [TestMethod]
        public void GetVertexValue_SetValue_ReturnsSetValue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var vertexKey = 1;
            var vertexValue = "test";
            weightedGraph.AddVertex(vertexKey);
            weightedGraph.SetVertexValue(
                vertexKey,
                vertexValue);

            var result = weightedGraph.GetVertexValue(vertexKey);

            result.ShouldBe(vertexValue);
        }

        [TestMethod]
        public void IsAdjacent_ExistingEdge_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var sourceKey = 1;
            var targetKey = 2;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = weightedGraph.IsAdjacent(
                sourceKey,
                targetKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void IsAdjacent_MissingVertex_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var missingVertexKey = 1;

            var result = weightedGraph.IsAdjacent(
                missingVertexKey,
                missingVertexKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void IsAdjacent_ReversedVertices_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var sourceKey = 1;
            var targetKey = 2;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = weightedGraph.IsAdjacent(
                targetKey,
                sourceKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void IsAdjacent_SelfEdgeVertex_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var vertexKey = 1;
            weightedGraph.AddVertex(vertexKey);
            weightedGraph.AddEdge(
                vertexKey,
                vertexKey);

            var result = weightedGraph.IsAdjacent(
                vertexKey,
                vertexKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveEdge_ExistingEdge_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var sourceKey = 1;
            var targetKey = 2;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = weightedGraph.RemoveEdge(
                sourceKey,
                targetKey);

            result.ShouldBeTrue();
        }


        [TestMethod]
        public void RemoveEdge_MissingVertex_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var missingVertexKey = 1;
            
            var result = weightedGraph.RemoveEdge(
                missingVertexKey,
                missingVertexKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_ReversedVertex_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var sourceKey = 1;
            var targetKey = 2;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddEdge(
                sourceKey,
                targetKey);

            var result = weightedGraph.RemoveEdge(
                targetKey,
                sourceKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_SelfEdgeVertex_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var vertexKey= 1;
            weightedGraph.AddVertex(vertexKey);
            weightedGraph.AddEdge(
                vertexKey,
                vertexKey);

            var result = weightedGraph.RemoveEdge(
                vertexKey,
                vertexKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveVertex_ExistingVertex_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var vertexKey = 1;
            weightedGraph.AddVertex(vertexKey);

            var result = weightedGraph.RemoveVertex(vertexKey);

            result.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveVertex_MissingVertex_ReturnsFalse()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var missingVertexKey = 1;

            var result = weightedGraph.RemoveVertex(missingVertexKey);

            result.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveVertex_ExistingEdges_ReturnsTrueAndRemovesEdges()
        {
            // TODO split into multiple RemoveVertex_ExistingEdges_... unit tests
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var sourceKey = 1;
            var targetKey = 2;
            var tertiaryKey = 3;
            weightedGraph.AddVertex(sourceKey);
            weightedGraph.AddVertex(targetKey);
            weightedGraph.AddVertex(tertiaryKey);
            weightedGraph.AddEdge(
                sourceKey, 
                targetKey);
            weightedGraph.AddEdge(
                targetKey,
                tertiaryKey);

            var result = weightedGraph.RemoveVertex(sourceKey);

            result.ShouldBeTrue();
            weightedGraph
                .GetNeighbors(sourceKey)
                .ShouldBeEmpty();
            weightedGraph.IsAdjacent(
                    sourceKey,
                    targetKey)
                .ShouldBeFalse();
            weightedGraph.IsAdjacent(
                    targetKey,
                    tertiaryKey)
                .ShouldBeTrue();
            weightedGraph
                .GetVertexValue(sourceKey)
                .ShouldBe(default(string));
        }

        [TestMethod]
        public void SetEdgeWeight_ExistingEdge_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();
            var missingSourceKey = 1;
            var missingTargetKey = 2;
            var edgeWeight = 3;
            weightedGraph.AddVertex(missingSourceKey);
            weightedGraph.AddVertex(missingTargetKey);
            weightedGraph.AddEdge(
                missingSourceKey,
                missingTargetKey);

            var setEdgeWeight = weightedGraph.SetEdgeWeight(
                missingSourceKey,
                missingTargetKey,
                edgeWeight);

            setEdgeWeight.ShouldBeTrue();
            weightedGraph
                .GetEdgeWeight(
                    missingSourceKey,
                    missingTargetKey)
                .ShouldBe(edgeWeight);
        }

        [TestMethod]
        public void SetEdgeWeight_MissingEdge_ThrowsKeyNotFound()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();
            var missingSourceKey = 1;
            var missingTargetKey = 2;
            var edgeWeight = 3;

            var exceptionMessage = Should.Throw<KeyNotFoundException>(
                    () =>
                    weightedGraph.SetEdgeWeight(
                        missingSourceKey,
                        missingTargetKey,
                        edgeWeight))
                .Message;
            exceptionMessage.ShouldContain($"Vertex (key: {missingSourceKey})");
            exceptionMessage.ShouldContain($"Vertex (key: {missingTargetKey})");
        }

        [TestMethod]
        public void SetVertexValue_ExistingVertex_ReturnsTrue()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var vertexKey = 1;
            var vertexValue = "test";
            weightedGraph.AddVertex(vertexKey);

            var result = weightedGraph.SetVertexValue(
                vertexKey,
                vertexValue);

            result.ShouldBeTrue();
            weightedGraph
                .GetVertexValue(vertexKey)
                .ShouldBe(vertexValue);
        }

        [TestMethod]
        public void SetVertexValue_MissingVertex_ThrowsKeyNotFound()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();;
            var missingVertexKey = 1;

            Should.Throw<KeyNotFoundException>(
                    () =>
                    weightedGraph.SetVertexValue(
                        missingVertexKey,
                        "test"))
                .Message
                .ShouldContain($"{missingVertexKey}");
        }
    }
}
