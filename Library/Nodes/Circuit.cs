using DP1.Library.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DP1.Library.Nodes
{
    public class Circuit : NodeBase
    {
        /// <inheritdoc />
        public Circuit(string nodeId) : base(nodeId)
        {
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            throw new NotImplementedException($"Circuit is not implemented yet. Id: {this.NodeId}");
        }
    }
}