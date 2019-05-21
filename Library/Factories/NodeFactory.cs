namespace DP1.Library.Factories
{
    using DP1.Library.Nodes;
    using System;

    public class NodeFactory
    {
        public NodeBase CreateNode(string id, string nodeType)
        {
            if(id == "")
            {
                throw new ArgumentException("Node id can't be empty.");
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
                    throw new ArgumentException("NAND Nodes are not yet supported. Error thrown for node: " + id + ".");
                case "NOR":
                    throw new ArgumentException("NOR Nodes are not yet supported. Error thrown for node: " + id + ".");
                case "XOR":
                    throw new ArgumentException("XOR Nodes are not yet supported. Error thrown for node: " + id + ".");
                default:
                    throw new ArgumentException("Node type not found. Error thrown for node: " + id + ".");
            }
        }
    }
}