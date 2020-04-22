namespace DP1.Library.Factories
{
    using File;
    using Nodes;
    using System;

    public class NodeFactory
    {
        public NodeBase CreateNode(string id, string nodeType)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Node id can't be empty.", nameof(id));
            }

            switch (nodeType)
            {
                case "INPUT_LOW":
                    return new InputNode(id, new State(false));
                case "INPUT_HIGH":
                    return new InputNode(id, new State(true));
                case "PROBE":
                    return new OutputNode(id);
                case "OR":
                    return new OrNode(id);
                case "AND":
                    return new AndNode(id);
                case "NOT":
                    return new NotNode(id);
                case "NAND":
                    return new NandNode(id);
                case "NOR":
                    return new NorNode(id);
                case "XOR":
                    return new XorNode(id);
                default:
                    throw new ArgumentException($"Node type not found. Error thrown for node: {id}.");
            }
        }

        public NodeBase CreateNode(NodeDefinition definition)
        {
            return this.CreateNode(definition.NodeId, definition.NodeType);
        }
    }
}