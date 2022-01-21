namespace Common.DataStructures.Tests.GraphsTests
{
    using Common.DataStructures.Contracts;
    using Common.DataStructures.Graphs.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System;
    using System.Linq;

    [TestClass]
    public class MatrixStorageTests
    {
        [TestMethod]
        public void AddEdge_AddSelfEdge_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            graphStorage.AddNode(sourceNodeKey);

            var addedEdge = graphStorage.AddEdge(
                sourceNodeKey,
                sourceNodeKey);

            addedEdge.ShouldBeTrue();
        }

        [TestMethod]
        public void AddEdge_MissingSourceNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var existingNodeKey = 0;
            var missingNodeKey = -1;
            graphStorage.AddNode(existingNodeKey);

            var addedEdge = graphStorage.AddEdge(
                missingNodeKey,
                existingNodeKey);

            addedEdge.ShouldBeFalse();
        }

        [TestMethod]
        public void AddEdge_MissingTargetNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var existingNodeKey = 0;
            var missingNodeKey = -1;
            graphStorage.AddNode(existingNodeKey);

            var addedEdge = graphStorage.AddEdge(
                existingNodeKey,
                missingNodeKey);

            addedEdge.ShouldBeFalse();
        }

        [TestMethod]
        public void AddEdge_NewEdge_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);

            var addedEdge = graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            addedEdge.ShouldBeTrue();
        }

        [TestMethod]
        public void AddNode_ExistingNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var existingNodeKey = 1;
            graphStorage.AddNode(existingNodeKey);

            var addedNode = graphStorage.AddNode(existingNodeKey);

            addedNode.ShouldBeFalse();
        }

        [TestMethod]
        public void AddNode_NewNode_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var addedNode = graphStorage.AddNode(0);

            addedNode.ShouldBeTrue();
        }

        [TestMethod]
        public void AreAdjacent_MissingSourceNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var missingSourceKey = -1;
            var existingTargetKey = 0;
            graphStorage.AddNode(existingTargetKey);

            var areAdjacent = graphStorage.AreAdjacent(
                missingSourceKey,
                existingTargetKey);

            areAdjacent.ShouldBeFalse();
        }

        [TestMethod]
        public void AreAdjacent_MissingTargetNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var existingSourceKey = 0;
            var missingTargetKey = -1;
            graphStorage.AddNode(existingSourceKey);

            var areAdjacent = graphStorage.AreAdjacent(
                existingSourceKey,
                missingTargetKey);

            areAdjacent.ShouldBeFalse();
        }

        [TestMethod]
        public void AreAdjacent_NodesAreAnEdge_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var areAdjacent = graphStorage.AreAdjacent(
                sourceNodeKey,
                targetNodeKey);

            areAdjacent.ShouldBeTrue();
        }

        [TestMethod]
        public void AreAdjacent_NodesAreNotAnEdge_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);

            var areAdjacent = graphStorage.AreAdjacent(
                sourceNodeKey,
                targetNodeKey);

            areAdjacent.ShouldBeFalse();
        }

        [TestMethod]
        public void GetEdges_NoEdges_ReturnsEmptyList()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var edges = graphStorage.GetEdges();

            edges.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetEdges_SingleDirectedEdge_ReturnsMatchingEdge()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: true);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var edges = graphStorage.GetEdges();

            edges.ShouldNotBeNull();
            edges.Count.ShouldBe(1);

            var edge = edges.First();
            edge.Source.ShouldBe(sourceNodeKey);
            edge.Target.ShouldBe(targetNodeKey);
            edge.Weight.ShouldBe(default);
        }

        [TestMethod]
        public void GetEdges_SingleDirectedSelfEdge_ReturnsSingleEdge()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: true);
            var sourceNodeKey = 0;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                sourceNodeKey);

            var edges = graphStorage.GetEdges();

            edges.ShouldNotBeNull();
            edges.Count.ShouldBe(1);
            var edge = edges.First();
            edge.Source.ShouldBe(edge.Target);
            edge.Weight.ShouldBe(default);
        }

        [TestMethod]
        public void GetEdges_SingleUnDirectedEdge_ReturnsReflectedEdges()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: false);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var edges = graphStorage.GetEdges();

            edges.ShouldNotBeNull();
            edges.Count.ShouldBe(2);

            edges.ToList().ForEach(
                edge =>
                {
                    edge.Source.ShouldBeOneOf(
                        sourceNodeKey,
                        targetNodeKey);
                    edge.Source.ShouldNotBe(edge.Target);
                    edge.Target.ShouldBeOneOf(
                        sourceNodeKey,
                        targetNodeKey);
                    edge.Weight.ShouldBe(default);
                });
        }

        [TestMethod]
        public void GetEdges_SingleUnDirectedSelfEdge_ReturnsSingleEdge()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: false);
            var sourceNodeKey = 0;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                sourceNodeKey);

            var edges = graphStorage.GetEdges();

            edges.ShouldNotBeNull();
            edges.Count.ShouldBe(1);
            var edge = edges.First();
            edge.Source.ShouldBe(edge.Target);
            edge.Weight.ShouldBe(default);
        }

        [TestMethod]
        public void GetEdgeWeight_DefaultEdge_ReturnsOffWeight()
        {
            var offWeight = -7;
            var graphStorage = GetGraphStorage<int, string, int>(
                offWeight: offWeight);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var edgeWeight = graphStorage.GetEdgeWeight(
                sourceNodeKey,
                targetNodeKey);

            edgeWeight.ShouldBe(offWeight);
        }

        [TestMethod]
        public void GetEdgeWeight_MissingSourceNode_ReturnsOffWeight()
        {
            var offWeight = -7;
            var graphStorage = GetGraphStorage<int, string, int>(
                offWeight: offWeight);
            var missingNodeKey = -1;
            var targetNodeKey = 0;
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                targetNodeKey,
                targetNodeKey);

            var edgeWeight = graphStorage.GetEdgeWeight(
                missingNodeKey,
                targetNodeKey);

            edgeWeight.ShouldBe(offWeight);
        }

        [TestMethod]
        public void GetEdgeWeight_MissingTargetNode_ReturnsOffWeight()
        {
            var offWeight = -7;
            var graphStorage = GetGraphStorage<int, string, int>(
                offWeight: offWeight);
            var sourceNodeKey = 0;
            var missingTargetKey = -1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                sourceNodeKey);

            var edgeWeight = graphStorage.GetEdgeWeight(
                sourceNodeKey,
                missingTargetKey);

            edgeWeight.ShouldBe(offWeight);
        }

        [TestMethod]
        public void GetEdgeWeight_SetEdgeWeight_RetainsValue()
        {
            var offWeight = -7;
            var graphStorage = GetGraphStorage<int, string, int>(
                offWeight: offWeight);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            var edgeWeight = 5;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);
            graphStorage.SetEdgeWeight(
                sourceNodeKey,
                targetNodeKey,
                edgeWeight);

            var newEdgeWeight = graphStorage.GetEdgeWeight(
                sourceNodeKey,
                targetNodeKey);

            newEdgeWeight.ShouldBe(edgeWeight);
        }

        private static IWeightedGraphStorage<TKey, TNode, TWeight> GetGraphStorage<TKey, TNode, TWeight>(
            bool isDirected = false,
            TWeight? offWeight = default)
            where TKey : IEquatable<TKey>
            where TWeight : notnull, IComparable<TWeight>
            => new MatrixGraphStorage<TKey, TNode, TWeight>(
                isDirected: isDirected,
                offWeight: offWeight);

        [TestMethod]
        public void GetNeighbors_MissingNodes_ReturnsEmptyList()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var neighbors = graphStorage.GetNeighbors(-1);

            neighbors.ShouldNotBeNull();
            neighbors.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetNeighbors_NoEdges_ReturnsEmptyList()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);

            var neighbors = graphStorage.GetNeighbors(sourceNodeKey);

            neighbors.ShouldNotBeNull();
            neighbors.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetNeighbors_SelfEdges_ReturnsSelf()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                sourceNodeKey);

            var neighbors = graphStorage.GetNeighbors(sourceNodeKey);

            neighbors.ShouldNotBeNull();
            neighbors.Count.ShouldBe(1);
            var neighbor = neighbors.First();
            neighbor.ShouldBe(sourceNodeKey);
        }

        [TestMethod]
        public void GetNeighbors_SingleEdge_ReturnsOneNeighbor()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var neighbors = graphStorage.GetNeighbors(sourceNodeKey);

            neighbors.ShouldNotBeNull();
            neighbors.Count.ShouldBe(1);
            var neighbor = neighbors.First();
            neighbor.ShouldBe(targetNodeKey);
        }

        [TestMethod]
        public void GetNodes_EmptyGraph_ReturnsEmptyList()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var nodes = graphStorage.GetNodes();

            nodes.ShouldNotBeNull();
            nodes.ShouldBeEmpty();
        }

        [TestMethod]
        public void GetNodes_AddedNode_ReturnsAddedNode()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            graphStorage.AddNode(sourceNodeKey);

            var nodes = graphStorage.GetNodes();

            nodes.ShouldNotBeNull();
            nodes.Count.ShouldBe(1);
            nodes.First().ShouldBe(sourceNodeKey);
        }

        [TestMethod]
        public void GetNodeValue_MissingNode_ReturnsNull()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var nodeValue = graphStorage.GetNodeValue(0);

            nodeValue.ShouldBeNull();
        }

        [TestMethod]
        public void GetNodeValue_NewNode_ReturnsDefaultValue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            graphStorage.AddNode(sourceNodeKey);

            var nodeValue = graphStorage.GetNodeValue(sourceNodeKey);

            nodeValue.ShouldBe(default);
        }

        [TestMethod]
        public void GetNodeValue_SetNode_ReturnsSetValue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var sourceNodeValue = "foo";
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.SetNodeValue(
                sourceNodeKey,
                sourceNodeValue);

            var nodeValue = graphStorage.GetNodeValue(sourceNodeKey);

            nodeValue.ShouldBe(sourceNodeValue);
        }

        [TestMethod]
        public void RemoveEdge_DirectedEdgeWithNoWeight_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: true);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var removedEdge = graphStorage.RemoveEdge(
                sourceNodeKey,
                targetNodeKey);

            removedEdge.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveEdge_DirectedEdgeWithWeight_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: true);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            var edgeWeight = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);
            graphStorage.SetEdgeWeight(
                sourceNodeKey,
                targetNodeKey,
                edgeWeight);

            var removedEdge = graphStorage.RemoveEdge(
                sourceNodeKey,
                targetNodeKey);

            removedEdge.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveEdge_MissingSourceNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var missingSourceKey = -1;
            var targetNodeKey = 0;
            graphStorage.AddNode(targetNodeKey);

            var removedEdge = graphStorage.RemoveEdge(
                missingSourceKey,
                targetNodeKey);

            removedEdge.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_MissingTargetNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var missingTargetKey = -1;
            graphStorage.AddNode(sourceNodeKey);

            var removedEdge = graphStorage.RemoveEdge(
                sourceNodeKey,
                missingTargetKey);

            removedEdge.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_NoEdges_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);

            var removedEdge = graphStorage.RemoveEdge(
                sourceNodeKey,
                targetNodeKey);

            removedEdge.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveEdge_UnDirectedEdgeWithNoWeight_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: false);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var removedEdge = graphStorage.RemoveEdge(
                sourceNodeKey,
                targetNodeKey);

            removedEdge.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveEdge_UnDirectedEdgeWithWeight_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>(
                isDirected: false);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            var edgeWeight = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);
            graphStorage.SetEdgeWeight(
                sourceNodeKey,
                targetNodeKey,
                edgeWeight);

            var removedEdge = graphStorage.RemoveEdge(
                sourceNodeKey,
                targetNodeKey);

            removedEdge.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveNode_ExistingNode_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var nodeKey = 0;
            graphStorage.AddNode(nodeKey);

            var removedNode = graphStorage.RemoveNode(nodeKey);

            removedNode.ShouldBeTrue();
        }

        [TestMethod]
        public void RemoveNode_MissingNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var removedNode = graphStorage.RemoveNode(0);

            removedNode.ShouldBeFalse();
        }

        [TestMethod]
        public void RemoveNode_SourceNodeFromEdge_RemovesEdges()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;

            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var removedNode = graphStorage.RemoveNode(sourceNodeKey);
            removedNode.ShouldBeTrue();
            var edges = graphStorage.GetEdges();
            edges.ShouldNotBeNull();
            edges.ShouldBeEmpty();
        }

        [TestMethod]
        public void RemoveNode_TargetNodeFromEdge_RemovesEdges()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;

            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var removedNode = graphStorage.RemoveNode(targetNodeKey);
            removedNode.ShouldBeTrue();
            var edges = graphStorage.GetEdges();
            edges.ShouldNotBeNull();
            edges.ShouldBeEmpty();
        }

        [TestMethod]
        public void SetEdgeWeight_AddedEdge_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            var edgeWeight = 1;

            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var setEdgeWeight = graphStorage.SetEdgeWeight(
                sourceNodeKey,
                targetNodeKey,
                edgeWeight);

            setEdgeWeight.ShouldBeTrue();
        }

        [TestMethod]
        public void SetEdgeWeight_EmptyGraph_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var missingSourceKey = -1;
            var targetNodeKey = 0;
            var edgeWeight = 1;

            var setEdgeWeight = graphStorage.SetEdgeWeight(
                missingSourceKey,
                targetNodeKey,
                edgeWeight);

            setEdgeWeight.ShouldBeFalse();
        }

        [TestMethod]
        public void SetEdgeWeight_MissingSourceNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var missingSourceKey = -1;
            var targetNodeKey = 0;
            var edgeWeight = 1;

            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                targetNodeKey,
                targetNodeKey);

            var setEdgeWeight = graphStorage.SetEdgeWeight(
                missingSourceKey,
                targetNodeKey,
                edgeWeight);

            setEdgeWeight.ShouldBeFalse();
        }

        [TestMethod]
        public void SetEdgeWeight_MissingTargetNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var missingTargetKey = -1;
            var edgeWeight = 1;

            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                sourceNodeKey);

            var setEdgeWeight = graphStorage.SetEdgeWeight(
                sourceNodeKey,
                missingTargetKey,
                edgeWeight);

            setEdgeWeight.ShouldBeFalse();
        }

        [TestMethod]
        public void SetNodeValue_ExistingNode_ReturnsTrue()
        {
            var graphStorage = GetGraphStorage<int, string, int>();
            var sourceNodeKey = 0;
            var nodeValue = "foo";
            graphStorage.AddNode(sourceNodeKey);

            var setNodeValue = graphStorage.SetNodeValue(
                sourceNodeKey,
                nodeValue);

            setNodeValue.ShouldBeTrue();
        }

        [TestMethod]
        public void SetNodeValue_MissingNode_ReturnsFalse()
        {
            var graphStorage = GetGraphStorage<int, string, int>();

            var setNodeValue = graphStorage.SetNodeValue(
                0,
                "foo");

            setNodeValue.ShouldBeFalse();
        }
    }
}
