namespace DP1.Library.Nodes
{
    using DP1.Library.Nodes;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DP1.Library.Factories;

    public class Circuit : NodeBase
    {
        /// <inheritdoc />
        public Circuit(string nodeId)
            : base(nodeId)
        {
        }

        public override void Register(NodeFactory factory)
        {
            // A circuit doesn't need to register.
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            throw new NotImplementedException($"Circuit is not implemented yet. Id: {this.NodeId}");
        }

        public override NodeBase Clone(string newId)
        {
            throw new NotImplementedException();
        }
    }
}