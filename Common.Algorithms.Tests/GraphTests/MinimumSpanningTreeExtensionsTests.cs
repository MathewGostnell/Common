namespace Common.Algorithms.Tests.GraphTests
{
    using Common.Algorithms.Graph;
    using Common.DataStructures.Graphs.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System.Linq;

    [TestClass]
    public class MinimumSpanningTreeExtensionsTests
    {
        [TestMethod]
        public void GetPrimsMinimumSpanningTree_SampleGraph_ReturnsExpectedTree()
        {
            var weightedGraph = new WeightedGraph<int, string, int>(
                new MatrixGraphStorage<int, string, int>(
                    offWeight: 0,
                    isDirected: false));
            weightedGraph.AddNode(0);
            weightedGraph.AddNode(1);
            weightedGraph.AddNode(2);
            weightedGraph.AddNode(3);
            weightedGraph.AddNode(4);
            weightedGraph.AddNode(5);
            weightedGraph.AddNode(6);
            weightedGraph.AddNode(7);
            weightedGraph.AddNode(8);

            weightedGraph.AddEdge(0, 1);
            weightedGraph.AddEdge(0, 7);
            weightedGraph.AddEdge(1, 2);
            weightedGraph.AddEdge(1, 7);
            weightedGraph.AddEdge(2, 3);
            weightedGraph.AddEdge(2, 5);
            weightedGraph.AddEdge(2, 8);
            weightedGraph.AddEdge(3, 4);
            weightedGraph.AddEdge(3, 5);
            weightedGraph.AddEdge(4, 5);
            weightedGraph.AddEdge(5, 6);
            weightedGraph.AddEdge(6, 8);
            weightedGraph.AddEdge(6, 7);
            weightedGraph.AddEdge(7, 8);


            weightedGraph.SetEdgeWeight(0, 1, 4);
            weightedGraph.SetEdgeWeight(0, 7, 8);
            weightedGraph.SetEdgeWeight(1, 2, 8);
            weightedGraph.SetEdgeWeight(1, 7, 11);
            weightedGraph.SetEdgeWeight(2, 3, 7);
            weightedGraph.SetEdgeWeight(2, 5, 4);
            weightedGraph.SetEdgeWeight(2, 8, 2);
            weightedGraph.SetEdgeWeight(3, 4, 9);
            weightedGraph.SetEdgeWeight(3, 5, 14);
            weightedGraph.SetEdgeWeight(4, 5, 10);
            weightedGraph.SetEdgeWeight(5, 6, 2);
            weightedGraph.SetEdgeWeight(6, 8, 6);
            weightedGraph.SetEdgeWeight(6, 7, 1);
            weightedGraph.SetEdgeWeight(7, 8, 7);

            var mst = weightedGraph.GetPrimsMinimumSpanningTree(
                0,
                int.MaxValue);

            mst.Count.ShouldBe(9);
            mst.Select(kvp => kvp.Value).Sum().ShouldBe(37);
        }
    }
}
