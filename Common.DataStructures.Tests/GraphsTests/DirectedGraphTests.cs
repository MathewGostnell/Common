namespace Common.DataStructures.Tests.GraphsTests
{
    using Common.DataStructures.Graphs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class DirectedGraphTests
    {
        [TestMethod]
        public void AddVertices_EmptyList_ReturnsFalse()
        {
            var directedGraph = new DirectedGraph<Edge<int>, int, string>();

            var addedAll = directedGraph.AddVertices();

            addedAll.ShouldBeFalse();
        }

        [TestMethod]
        public void AddVertices_NoRepeats_ReturnsTrue()
        {
            var directedGraph = new DirectedGraph<Edge<int>, int, string>();

            var addedAll = directedGraph.AddVertices(0, 1, 3);

            addedAll.ShouldBeTrue();
        }

        [TestMethod]
        public void DepthFirstSearch_LinearPath_ReturnsFullPath()
        {
            var directedGraph = new DirectedGraph<Edge<int>, int, string>();
            directedGraph.AddVertices(0, 1, 2, 3, 4, 5);

            directedGraph.AddEdge(0, 1);
            directedGraph.AddEdge(1, 2);
            directedGraph.AddEdge(1, 3);
            directedGraph.AddEdge(1, 4);
            directedGraph.AddEdge(4, 5);

            var result = directedGraph.DepthFirstSearch(
                0, 
                5);

            result.ShouldNotBeEmpty();
            result.Count.ShouldBe(4);
            result[0].ShouldBe(0);
            result[1].ShouldBe(1);
            result[2].ShouldBe(4);
            result[3].ShouldBe(5);
        }
    }
}
