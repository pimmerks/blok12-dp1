namespace DP1.Library.Factories
{
    using File;
    using Nodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NodeFactory
    {
        private static NodeFactory instance;
        public static NodeFactory Instance => NodeFactory.instance ?? (NodeFactory.instance = new NodeFactory());

        private readonly Dictionary<string, NodeBase> nodeTypes = new Dictionary<string, NodeBase>();

        private NodeFactory()
        {
            var nodes = typeof(NodeBase).Assembly.DefinedTypes
                            .Where(x => typeof(NodeBase).IsAssignableFrom(x))
                            .Where(x => x != typeof(NodeBase))
                            .ToList();

            foreach (var nodeType in nodes)
            {
                if (!(Activator.CreateInstance(nodeType, "") is NodeBase node))
                {
                    throw new Exception($"Could not create instance of {nodeType}");
                }

                node.Register(this);
            }
        }

        public NodeBase CreateNode(string id, string nodeType)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Node id can't be empty.", nameof(id));
            }
            
            // Special case for input node:
            if (nodeType.StartsWith("INPUT_"))
            {
                return InputNodeFactory.Instance.CreateNode(id, nodeType);
            }

            if (!this.nodeTypes.ContainsKey(nodeType))
            {
                throw new ArgumentException($"Node type '{nodeType}' not found. Error thrown for node: {id}.");
            }

            var node = this.nodeTypes[nodeType].Clone(id);
            return node;
        }
        
        public NodeBase CreateNode(NodeDefinition definition)
        {
            return this.CreateNode(definition.NodeId, definition.NodeType);
        }

        public void RegisterNode(string nodeType, NodeBase node)
        {
            this.nodeTypes.Add(nodeType, node);
        }

        public bool ContainsType(string type)
        {
            return this.nodeTypes.ContainsKey(type) || InputNodeFactory.Instance.ContainsType(type);
        }
    }
}