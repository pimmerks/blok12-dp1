using System;
using System.Collections.Generic;
using System.Linq;

namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public class OutputNode : NodeBase
    {
        /// <inheritdoc />
        public OutputNode(string nodeId)
            : base(nodeId)
        {
        }

        public override void Register(NodeFactory factory)
        {
            factory.RegisterNode("PROBE", new OutputNode(""));
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length != 1)
            {
                throw new Exception($"An OutputNode can only contain 1 input. Id: {this.NodeId}");
            }

            this.CurrentState = new State(input[0].LogicState);
        }

        public override NodeBase Clone(string nodeId)
        {
            return new OutputNode(nodeId)
            {
                CurrentState = this.CurrentState,
            };
        }
    }
}