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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>> mutableGraph
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
        MutableGraph<int, TestEdge<int>> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: true);

        int newEdgeCount = mutableGraph.AddEdges(null);

        newEdgeCount.ShouldBe(0);
    }

    [TestMethod]
    public void AddEdges_UnDirectedGraphDuplicateEdges_ReturnsTwo()
    {
        MutableGraph<int, TestEdge<int>> mutableGraph
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
    public void AddNode_DuplicatNodeKey_ReturnsFalse()
    {
        MutableGraph<int, TestEdge<int>> mutableGraph
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
        MutableGraph<int, TestEdge<int>> mutableGraph
            = GetMutableGraph<int, TestEdge<int>, int>(
                isDirected: false);

        bool addedNode = mutableGraph.AddNode(0);

        addedNode.ShouldBeTrue();
    }

    [TestMethod]
    public void AddNodes_DuplicateNodeKey_ReturnsOne()
    {
        MutableGraph<int, TestEdge<int>> mutableGraph
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
        MutableGraph<int, TestEdge<int>> mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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
        MutableGraph<int, TestEdge<int>>? mutableGraph
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

    protected MutableGraph<TKey, TEdge> GetMutableGraph<TKey, TEdge, TValue>(
        bool isDirected = false)
        where TEdge : IEdge<TKey>
        where TKey : IEquatable<TKey>
    {
        return new MutableGraph<TKey, TEdge>(
            new MatrixNodeSet<TKey, TValue>(),
            new MatrixEdgeSet<TKey, TEdge>(
                new EdgeFactory<TKey, TEdge>()),
            new EdgeFactory<TKey, TEdge>(),
            isDirected);
    }
}