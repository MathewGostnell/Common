namespace Common.DataStructures.Tests.GraphsTests;

using Common.DataStructures.Graphs.Contracts;
using Common.ExtensionLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;
using System.Collections.Generic;

[TestClass]
public class NodeSetTests
{
    [TestMethod]
    public void INodeSet_AfterConstruction_HasDefaultValue()
    {
        INodeSet<int> nodeSet = GetInterfaceStub();

        nodeSet.AreNodesEmpty.ShouldBeTrue();
        nodeSet.NodeCount.ShouldBe(0);
        nodeSet.Nodes.IsEmpty()
            .ShouldBe(nodeSet.AreNodesEmpty);
    }

    protected INodeSet<int> GetInterfaceStub()
    {
        var stubNodeSet = new Mock<INodeSet<int>>();
        stubNodeSet.Setup(
                nodeSet =>
                nodeSet.AreNodesEmpty)
            .Returns(true);
        stubNodeSet.Setup(
                nodeSet =>
                nodeSet.NodeCount)
            .Returns(0);
        stubNodeSet.Setup(
                nodeSet =>
                nodeSet.Nodes)
            .Returns(new List<int>());

        return stubNodeSet.Object;
    }
}