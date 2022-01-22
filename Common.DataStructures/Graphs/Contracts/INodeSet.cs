namespace Common.DataStructures.Graphs.Contracts
{
    using System.Collections.Generic;

    public interface INodeSet<TKey> : IImplicitNodeSet<TKey>
    {
        public bool AreNodesEmpty
        {
            get;
        }

        public int NodeCount
        {
            get;
        }

        public IEnumerable<TKey> Nodes
        {
            get;
        }
    }
}
