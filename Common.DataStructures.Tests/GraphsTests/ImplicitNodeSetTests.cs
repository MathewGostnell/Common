namespace Common.DataStructures.Tests.GraphsTests;

using Common.DataStructures.Graphs.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

[TestClass]
public class ImplicitNodeSetTests : GraphsTestsHelper
{
    protected const int ExistingNodeKey = 1;
    protected const int MissingNodeKey = -1;

    [TestMethod]
    public void ContainsNode_ExistinNode_ReturnsTrue()
    {
        IImplicitNodeSet<int>? implicitNodeSet = GetInterfaceStub();

        bool containsNode = implicitNodeSet.ContainsNode(ExistingNodeKey);

        containsNode.ShouldBeTrue();
    }

    [TestMethod]
    public void ContainsNode_MissingNode_ReturnsFalse()
    {
        IImplicitNodeSet<int>? implicitNodeSet = GetInterfaceStub();

        bool containsNode = implicitNodeSet.ContainsNode(MissingNodeKey);

        containsNode.ShouldBeFalse();
    }

    protected virtual IImplicitNodeSet<int> GetInterfaceStub()
        => GetInterfaceStub(
            new Mock<IImplicitNodeSet<int>>());

    protected virtual IImplicitNodeSet<int> GetInterfaceStub(
        Mock<IImplicitNodeSet<int>> stubImplicitNodeSet)
    {
        stubImplicitNodeSet
            .Setup(
                implicitNodeSet =>
                    implicitNodeSet.ContainsNode(ExistingNodeKey))
            .Returns(true);

        return stubImplicitNodeSet.Object;
    }
}