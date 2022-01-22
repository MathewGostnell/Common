namespace Common.DataStructures.Tests.GraphsTests;

using Common.DataStructures.Graphs.Collections;
using Common.DataStructures.Graphs.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
public class GraphTests
{
    [TestMethod]
    public void IsDirected_DefaultGraph_IsTrue()
    {
        IGraph<int, IEdge<int>> graph = new AdjacencyGraph<int, IEdge<int>>();
        graph.IsDirected.ShouldBeTrue();
    }
}