namespace DP1.Library.Simulation
{
    using Nodes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;

    public class NodeConnection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.OutputNode)));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.InputNodes)));
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.String)));

            return this.OutputNode.CurrentState;
        }

        public string String => this.ToString();

        public override string ToString()
        {
            return $"{string.Concat(this.InputNodes.Select(x => $"{x.NodeId},"))} -> {this.OutputNode.NodeId}";
        }
    }
}