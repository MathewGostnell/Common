namespace Common.DataStructures.Tests.GraphsTests;

using Common.DataStructures.Graphs.Contracts;
using Common.DataStructures.Graphs.Edges;
using Common.DataStructures.Graphs.Graphs;
using Common.DataStructures.Graphs.Nodes;
using Common.DataStructures.Tests.GraphsTests.Edges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

[TestClass]
public class MutableGraphTests
{
    [TestMethod]
    public void AddEdge_DirectedGraph_AddsOneEdge()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        int sourceNode = 0;
        int targetNode = 1;
        mutableGraph
            .AddNode(sourceNode)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNode)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                sourceNode,
                targetNode)
            .ShouldBeTrue();

        int edgeCount = mutableGraph.Edges.Count();

        edgeCount.ShouldBe(1);
    }

    [TestMethod]
    public void AddEdge_ExistingEdge_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNode = 0;
        int targetNode = 1;
        mutableGraph
            .AddNode(sourceNode)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNode)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
                sourceNode,
                targetNode)
            .ShouldBeTrue();

        bool addedEdge = mutableGraph.AddEdge(
            sourceNode,
            targetNode);

        addedEdge.ShouldBeFalse();
    }

    [TestMethod]
    public void AddEdge_ExistingNodes_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNode = 0;
        int targetNode = 1;
        mutableGraph.AddNode(sourceNode);
        mutableGraph.AddNode(targetNode);

        bool addedEdge = mutableGraph.AddEdge(
            sourceNode,
            targetNode);

        addedEdge.ShouldBeTrue();
    }

    [TestMethod]
    public void AddEdge_MissingSourceNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int missingNode = -1;
        int targetNode = 0;
        mutableGraph.AddNode(missingNode);

        bool addedEdge = mutableGraph.AddEdge(
            missingNode,
            targetNode);

        addedEdge.ShouldBeFalse();
    }

    [TestMethod]
    public void AddEdge_MissingTargetNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNode = 0;
        int missingTarget = -1;
        mutableGraph.AddNode(sourceNode);

        bool addedEdge = mutableGraph.AddEdge(
            sourceNode,
            missingTarget);

        addedEdge.ShouldBeFalse();
    }

    [TestMethod]
    public void AddEdge_UnDirectedGraph_AddsTwoEdges()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNode = 0;
        int targetNode = 1;
        mutableGraph
            .AddNode(sourceNode)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNode)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                sourceNode,
                targetNode)
            .ShouldBeTrue();

        int edgeCount = mutableGraph.Edges.Count();

        edgeCount.ShouldBe(2);
    }

    [TestMethod]
    public void AddEdges_DirectedGraphDuplicateEdges_ReturnsOne()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        var duplicateEdge = new TestEdge<int>(
            0,
            1);
        int newEdgeCount = mutableGraph.AddEdges(
            new List<TestEdge<int>>
            {
                duplicateEdge,
                duplicateEdge,
                duplicateEdge,
                duplicateEdge,
                duplicateEdge,
                duplicateEdge
            });

        newEdgeCount.ShouldBe(1);
    }

    [TestMethod]
    public void AddEdges_NullEnumerable_ReturnsZero()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);

        int newEdgeCount = mutableGraph.AddEdges(null);

        newEdgeCount.ShouldBe(0);
    }

    [TestMethod]
    public void AddEdges_UnDirectedGraphDuplicateEdges_ReturnsTwo()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        var duplicateEdge = new TestEdge<int>(
            0,
            1);
        int newEdgeCount = mutableGraph.AddEdges(
            new List<TestEdge<int>>
            {
                duplicateEdge,
                duplicateEdge,
                duplicateEdge,
                duplicateEdge,
                duplicateEdge,
                duplicateEdge
            });

        newEdgeCount.ShouldBe(2);
    }

    [TestMethod]
    public void AddNode_DuplicateNodeKey_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int nodeKey = 0;
        mutableGraph.AddNode(nodeKey).ShouldBeTrue();

        bool addedNode = mutableGraph.AddNode(nodeKey);

        addedNode.ShouldBeFalse();
    }

    [TestMethod]
    public void AddNode_NewNodeKey_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);

        bool addedNode = mutableGraph.AddNode(0);

        addedNode.ShouldBeTrue();
    }

    [TestMethod]
    public void AddNodes_DuplicateNodeKey_ReturnsOne()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int duplicateNodeKey = 0;

        int addedNodeCount = mutableGraph.AddNodes(
            new List<int>
            {
                duplicateNodeKey,
                duplicateNodeKey
            });

        addedNodeCount.ShouldBe(1);
    }

    [TestMethod]
    public void AddNodes_NewNodesKey_ReturnsOne()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int nodeKeyIndex = 0;

        int addedNodeCount = mutableGraph.AddNodes(
            new List<int>
            {
                nodeKeyIndex++,
                nodeKeyIndex++,
                nodeKeyIndex++,
                nodeKeyIndex++,
                nodeKeyIndex++
            });

        addedNodeCount.ShouldBe(nodeKeyIndex);
    }

    [TestMethod]
    public void AreAdjacent_DirectedEdge_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
                sourceNodeKey,
                targetNodeKey)
            .ShouldBeTrue();

        bool areAdjacent = mutableGraph.AreAdjacent(
            sourceNodeKey,
            targetNodeKey);

        areAdjacent.ShouldBeTrue();
    }

    [TestMethod]
    public void AreAdjacent_DirectedEdgeReflected_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
                sourceNodeKey,
                targetNodeKey)
            .ShouldBeTrue();

        bool areAdjacent = mutableGraph.AreAdjacent(
            targetNodeKey,
            sourceNodeKey);

        areAdjacent.ShouldBeFalse();
    }

    [TestMethod]
    public void AreAdjacent_MissingSourceNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int missingSourceKey = -1;
        int targetNodeKey = 0;
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                missingSourceKey,
                targetNodeKey)
            .ShouldBeFalse();

        bool areAdjacent = mutableGraph.AreAdjacent(
            missingSourceKey,
            targetNodeKey);

        areAdjacent.ShouldBeFalse();
    }

    [TestMethod]
    public void AreAdjacent_MissingTargetNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNodeKey = 0;
        int missingTargetNode = -1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
                sourceNodeKey,
                missingTargetNode)
            .ShouldBeFalse();

        bool areAdjacent = mutableGraph.AreAdjacent(
            sourceNodeKey,
            missingTargetNode);

        areAdjacent.ShouldBeFalse();
    }

    [TestMethod]
    public void AreAdjacent_UnDirectedEdgeReflected_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
                sourceNodeKey,
                targetNodeKey)
            .ShouldBeTrue();

        bool areAdjacent = mutableGraph.AreAdjacent(
            targetNodeKey,
            sourceNodeKey);

        areAdjacent.ShouldBeTrue();
    }

    [TestMethod]
    public void Clear_DefaultGraph_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);

        bool didClear = mutableGraph.Clear();

        didClear.ShouldBeTrue();
    }

    [TestMethod]
    public void Clear_PopulatedEdgeGraph_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
                sourceNodeKey,
                targetNodeKey)
            .ShouldBeTrue();

        bool didClear = mutableGraph.Clear();

        didClear.ShouldBeTrue();
    }

    [TestMethod]
    public void Clear_PopulatedNodeGraph_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();

        bool didClear = mutableGraph.Clear();

        didClear.ShouldBeTrue();
    }

    [TestMethod]
    public void GetNeighbors_DirectedGraphEdge_ReturnsOneEdge()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph.AddNode(sourceNodeKey);
        mutableGraph.AddNode(targetNodeKey);
        mutableGraph.AddEdge(
            sourceNodeKey,
            targetNodeKey);

        IEnumerable<TestEdge<int>>? nodeNeighbors
            = mutableGraph.GetNeighbors(sourceNodeKey);

        nodeNeighbors.ShouldNotBeEmpty();
        nodeNeighbors.Count().ShouldBe(1);
        nodeNeighbors.First().TargetKey.ShouldBe(targetNodeKey);
    }

    [TestMethod]
    public void GetNeighbors_ExistingEdges_ReturnsNeighbors()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        int tertiaryNodeKey = 2;
        mutableGraph.AddNode(sourceNodeKey);
        mutableGraph.AddNode(targetNodeKey);
        mutableGraph.AddNode(tertiaryNodeKey);
        mutableGraph.AddEdge(
            sourceNodeKey,
            targetNodeKey);
        mutableGraph.AddEdge(
            sourceNodeKey,
            tertiaryNodeKey);

        IEnumerable<TestEdge<int>>? nodeNeighbors
            = mutableGraph.GetNeighbors(sourceNodeKey);

        nodeNeighbors.ShouldNotBeEmpty();
        nodeNeighbors.Count().ShouldBe(2);
        nodeNeighbors.ToList().ForEach(
            neighborEdge =>
            {
                neighborEdge.SourceKey.ShouldBe(sourceNodeKey);
                neighborEdge.TargetKey.ShouldNotBe(sourceNodeKey);
            });
    }

    [TestMethod]
    public void GetNeighbors_MissingNode_ReturnsEmptyList()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int missingSourceNode = 0;

        IEnumerable<TestEdge<int>>? nodeNeighbors
            = mutableGraph.GetNeighbors(missingSourceNode);

        nodeNeighbors.ShouldBeEmpty();
    }

    [TestMethod]
    public void GetNeighbors_NoNeighborsNode_ReturnsEmptyList()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        mutableGraph.AddNode(sourceNodeKey);

        IEnumerable<TestEdge<int>>? nodeNeighbors
            = mutableGraph.GetNeighbors(sourceNodeKey);

        nodeNeighbors.ShouldBeEmpty();
    }

    [TestMethod]
    public void GetNeighbors_SelfEdge_ReturnsSelfEdge()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                sourceNodeKey,
                sourceNodeKey)
            .ShouldBeTrue();

        IEnumerable<TestEdge<int>>? nodeNeighbors
            = mutableGraph.GetNeighbors(sourceNodeKey);

        nodeNeighbors.ShouldNotBeEmpty();
        nodeNeighbors.Count().ShouldBe(1);
        nodeNeighbors.ToList().ForEach(
            neighborEdge =>
            {
                neighborEdge.SourceKey.ShouldBe(sourceNodeKey);
                neighborEdge.TargetKey.ShouldBe(sourceNodeKey);
            });
    }

    [TestMethod]
    public void GetNeighbors_UnDirectedGraphEdge_ReturnsOne()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph.AddNode(sourceNodeKey);
        mutableGraph.AddNode(targetNodeKey);
        mutableGraph.AddEdge(
            sourceNodeKey,
            targetNodeKey);

        IEnumerable<TestEdge<int>>? nodeNeighbors
            = mutableGraph.GetNeighbors(sourceNodeKey);

        nodeNeighbors.ShouldNotBeEmpty();
        nodeNeighbors.Count().ShouldBe(1);
        nodeNeighbors.First().TargetKey.ShouldBe(targetNodeKey);
    }

    [TestMethod]
    public void GetNodeValue_ExistingNode_ReturnsDefaultValue()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int nodeKey = 1;
        mutableGraph.AddNode(nodeKey);

        int nodeValue = mutableGraph.GetNodeValue(nodeKey);

        nodeValue.ShouldBe(default);
    }

    [TestMethod]
    public void GetNodeValue_MissingNode_ReturnsDefaultValue()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int missingNodeKey = -1;

        int nodeValue = mutableGraph.GetNodeValue(missingNodeKey);

        nodeValue.ShouldBe(default);
    }

    [TestMethod]
    public void GetNodeValue_SetNodeValue_ReturnsSetValue()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int nodeKey = 1;
        int nodeValue = 42;
        mutableGraph
            .AddNode(nodeKey)
            .ShouldBeTrue();
        mutableGraph
            .SetNodeValue(
                nodeKey,
                nodeValue)
            .ShouldBeTrue();

        int value = mutableGraph.GetNodeValue(nodeKey);

        nodeValue.ShouldBe(nodeValue);
    }

    [TestMethod]
    public void RemoveEdge_ExistingEdge_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph.AddEdge(
            sourceNodeKey,
            targetNodeKey);

        bool removedEdge = mutableGraph.RemoveEdge(
            sourceNodeKey,
            targetNodeKey);

        removedEdge.ShouldBeTrue();
    }

    [TestMethod]
    public void RemoveEdge_MissingEdge_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();

        bool removedEdge = mutableGraph.RemoveEdge(
            sourceNodeKey,
            targetNodeKey);

        removedEdge.ShouldBeFalse();
    }

    [TestMethod]
    public void RemoveEdge_MissingSourceNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int missingNodeKey = -1;
        int targetNodeKey = 0;
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                missingNodeKey,
                targetNodeKey)
            .ShouldBeFalse();

        bool removedEdge = mutableGraph.RemoveEdge(
            missingNodeKey,
            targetNodeKey);

        removedEdge.ShouldBeFalse();
    }

    [TestMethod]
    public void RemoveEdge_MissingTargetNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>();
        int sourceNodeKey = 0;
        int missingTargetNode = -1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                sourceNodeKey,
                missingTargetNode)
            .ShouldBeFalse();

        bool removedEdge = mutableGraph.RemoveEdge(
            sourceNodeKey,
            missingTargetNode);

        removedEdge.ShouldBeFalse();
    }

    [TestMethod]
    public void RemoveEdges_DirectedDuplicateEdge_ReturnsOne()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                sourceNodeKey,
                targetNodeKey)
            .ShouldBeTrue();

        int removedEdgeCount = mutableGraph.RemoveEdges(
            new List<TestEdge<int>>
            {
                new TestEdge<int>(sourceNodeKey, targetNodeKey),
                new TestEdge<int>(sourceNodeKey, targetNodeKey)
            });

        removedEdgeCount.ShouldBe(1);
    }

    [TestMethod]
    public void RemoveEdges_UnDirectedDuplicateEdge_ReturnsTwo()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddEdge(
                sourceNodeKey,
                targetNodeKey)
            .ShouldBeTrue();

        int removedEdgeCount = mutableGraph.RemoveEdges(
            new List<TestEdge<int>>
            {
                new TestEdge<int>(sourceNodeKey, targetNodeKey),
                new TestEdge<int>(sourceNodeKey, targetNodeKey)
            });

        removedEdgeCount.ShouldBe(2);
    }

    [TestMethod]
    public void RemoveNode_ExistingNodeKey_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int nodeKey = 0;
        mutableGraph.AddNode(nodeKey).ShouldBeTrue();

        bool removedNode = mutableGraph.RemoveNode(nodeKey);

        removedNode.ShouldBeTrue();
    }

    [TestMethod]
    public void RemoveNode_MissingNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int missingNodeKey = -1;

        bool removedNode = mutableGraph.RemoveNode(missingNodeKey);

        removedNode.ShouldBeFalse();
    }

    [TestMethod]
    public void RemoveEdges_UniqueEdges_ReturnsCount()
    {
        MutableGraph<int, TestEdge<int>, int> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 0;
        int targetNodeKey = 1;
        int tertiaryNodeKey = 2;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(targetNodeKey)
            .ShouldBeTrue();
        mutableGraph
            .AddNode(tertiaryNodeKey)
            .ShouldBeTrue();
        var testEdgeList = new List<TestEdge<int>>
        {
            new TestEdge<int>(
                    sourceNodeKey,
                    targetNodeKey),
                new TestEdge<int>(
                    sourceNodeKey,
                    tertiaryNodeKey)
        };
        int addedEdges = mutableGraph.AddEdges(testEdgeList);

        int removedEdgeCount = mutableGraph.RemoveEdges(testEdgeList);

        removedEdgeCount.ShouldBe(addedEdges);
    }

    [TestMethod]
    public void RemoveNodes_DuplicateNodeKey_ReturnsCount()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int duplicateNodeKey = 0;
        var testNodeList = new List<int>
        {
            duplicateNodeKey,
            duplicateNodeKey
        };
        int addedNodeCount = mutableGraph.AddNodes(testNodeList);

        int removedNodeCount = mutableGraph.RemoveNodes(testNodeList);

        addedNodeCount.ShouldBe(addedNodeCount);
    }

    [TestMethod]
    public void RemovesNodes_NewNodesKey_ReturnsCount()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int priamryNodeKey = 0;
        int secondaryNodeKey = 1;
        var testNodeList = new List<int>
        {
            priamryNodeKey,
            secondaryNodeKey
        };
        int addedNodeCount = mutableGraph.AddNodes(testNodeList);

        int removedNodeCount = mutableGraph.RemoveNodes(testNodeList);

        addedNodeCount.ShouldBe(addedNodeCount);
    }

    [TestMethod]
    public void SetNodeValue_ExistingNode_ReturnsTrue()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int sourceNodeKey = 1;
        int nodeValue = 42;
        mutableGraph
            .AddNode(sourceNodeKey)
            .ShouldBeTrue();

        bool setNodeValue = mutableGraph.SetNodeValue(
            sourceNodeKey,
            nodeValue);

        setNodeValue.ShouldBeTrue();
    }

    [TestMethod]
    public void SetNodeValue_MissingNode_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>, int>? mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);
        int missingNodeKey = -1;
        int nodeValue = 42;

        bool setNodeValue = mutableGraph.SetNodeValue(
            missingNodeKey,
            nodeValue);

        setNodeValue.ShouldBeFalse();
    }

    protected static MutableGraph<TKey, TEdge, TValue> GetMutableGraph<TKey, TEdge, TValue>(
        bool isDirected = false)
        where TEdge : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        return new MutableGraph<TKey, TEdge, TValue>(
            new MatrixNodeSet<TKey, TValue>(),
            new MatrixEdgeSet<TKey, TEdge>(
                new EdgeFactory<TKey, TEdge>()),
            new EdgeFactory<TKey, TEdge>(),
            isDirected);
    }
}