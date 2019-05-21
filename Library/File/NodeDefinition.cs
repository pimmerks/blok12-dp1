namespace DP1.Library.File
{
    public class NodeDefinition : ParsedLine
    {
        public string NodeId { get; }

        public string NodeType { get; }

        public NodeDefinition(string nodeId, string nodeType)
        {
            this.NodeId = nodeId;
            this.NodeType = nodeType;
        }
    }
}
