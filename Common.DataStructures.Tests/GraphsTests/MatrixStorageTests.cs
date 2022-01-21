namespace Common.DataStructures.Tests.GraphsTests
{
    using Common.DataStructures.Graphs.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class MatrixStorageTests
    {
        [TestMethod]
        public void AddEdge_AddedEdgeWithoutSettingWeight_RetainsInEdges()
        {
            var graphStorage = new MatrixGraphStorage<int, string, int>(
                offWeight: 0,
                isDirected: true);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);
            graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            var edges = graphStorage.GetEdges();

            edges.Count.ShouldBe(0);
        }

        [TestMethod]
        public void AddEdge_MissingNode_ReturnsFalse()
        {
            var graphStorage = new MatrixGraphStorage<int, string, int>(-1);

            var addedEdge = graphStorage.AddEdge(-1, 0);

            addedEdge.ShouldBeFalse();
        }

        [TestMethod]
        public void AddEdge_NewEdge_ReturnsTrue()
        {
            var graphStorage = new MatrixGraphStorage<int, string, int>(-1);
            var sourceNodeKey = 0;
            var targetNodeKey = 1;
            graphStorage.AddNode(sourceNodeKey);
            graphStorage.AddNode(targetNodeKey);

            var addedEdge = graphStorage.AddEdge(
                sourceNodeKey,
                targetNodeKey);

            addedEdge.ShouldBeTrue();
        }
    }
}
