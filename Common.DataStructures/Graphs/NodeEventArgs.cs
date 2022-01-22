namespace Common.DataStructures.Graphs
{
    public class NodeEventArgs<TKey> : EventArgs
    {
        public NodeEventArgs(
            TKey nodeKey)
        {
            if (nodeKey is null)
            {
                throw new ArgumentNullException(nameof(nodeKey));
            }

            this.nodeKey = nodeKey;
        }

        private readonly TKey nodeKey;

        public TKey NodeKey => nodeKey;
    }
}
