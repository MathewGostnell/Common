namespace Common.Algorithms.Tests.GraphTests
{
    using Common.Algorithms.Graph;
    using Common.DataStructures.Graph;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System.Linq;

    [TestClass]
    public class MinimumSpanningTreeExtensionsTests
    {
        [TestMethod]
        public void GetPrimsMinimumSpanningTree_SampleGraph_ReturnsExpectedTree()
        {
            var weightedGraph = new WeightedGraph<int, string, int>();
            weightedGraph.AddVertex(0);
            weightedGraph.AddVertex(1);
            weightedGraph.AddVertex(2);
            weightedGraph.AddVertex(3);
            weightedGraph.AddVertex(4);
            weightedGraph.AddVertex(5);
            weightedGraph.AddVertex(6);
            weightedGraph.AddVertex(7);
            weightedGraph.AddVertex(8);

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
            weightedGraph.AddEdge(1, 0);
            weightedGraph.AddEdge(7, 0);
            weightedGraph.AddEdge(2, 1);
            weightedGraph.AddEdge(7, 1);
            weightedGraph.AddEdge(3, 2);
            weightedGraph.AddEdge(5, 2);
            weightedGraph.AddEdge(8, 2);
            weightedGraph.AddEdge(4, 3);
            weightedGraph.AddEdge(5, 3);
            weightedGraph.AddEdge(5, 4);
            weightedGraph.AddEdge(6, 5);
            weightedGraph.AddEdge(8, 6);
            weightedGraph.AddEdge(7, 6);
            weightedGraph.AddEdge(8, 7);


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

            weightedGraph.SetEdgeWeight(1, 0, 4);
            weightedGraph.SetEdgeWeight(7, 0, 8);
            weightedGraph.SetEdgeWeight(2, 1, 8);
            weightedGraph.SetEdgeWeight(7, 1, 11);
            weightedGraph.SetEdgeWeight(3, 2, 7);
            weightedGraph.SetEdgeWeight(5, 2, 4);
            weightedGraph.SetEdgeWeight(8, 2, 2);
            weightedGraph.SetEdgeWeight(4, 3, 9);
            weightedGraph.SetEdgeWeight(5, 3, 14);
            weightedGraph.SetEdgeWeight(5, 4, 10);
            weightedGraph.SetEdgeWeight(6, 5, 2);
            weightedGraph.SetEdgeWeight(8, 6, 6);
            weightedGraph.SetEdgeWeight(7, 6, 1);
            weightedGraph.SetEdgeWeight(8, 7, 7);

            var mst = weightedGraph.GetPrimsMinimumSpanningTree(
                0,
                int.MaxValue);

            mst.Count.ShouldBe(9);
            mst.Select(kvp => kvp.Value).Sum().ShouldBe(37);
        }
    }
}
