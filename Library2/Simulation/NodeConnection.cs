namespace DP1.Library.Simulation
{
    using DP1.Library.Nodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NodeConnection
    {
        public NodeBase OutputNode { get; set; }

        public List<NodeBase> InputNodes { get; set; }

        public NodeConnection(List<NodeBase> inputNodes, NodeBase outputNode)
        {
            this.InputNodes = inputNodes;
            this.OutputNode = outputNode;
        }

        public NodeConnection(NodeBase inputNode, NodeBase outputNode)
        {
            this.InputNodes = new List<NodeBase> { inputNode };
            this.OutputNode = outputNode;
        }

        public State GetResultFromOutputNode()
        {
            this.OutputNode.Calculate(
                this.InputNodes.Select(x => x.CurrentState).ToArray());

            return OutputNode.CurrentState;
        }
    }
}