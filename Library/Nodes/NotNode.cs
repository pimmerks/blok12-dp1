using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public class NotNode : NodeBase
    {
        /// <inheritdoc />
        public NotNode(string nodeId)
            : base(nodeId)
        {
        }

        public override void Register(NodeFactory factory)
        {
            factory.RegisterNode("NOT", new NotNode(""));
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length != 1)
            {
                throw new Exception($"A NotNode Can only contain 1 input. Id: {this.NodeId}");
            }

            this.CurrentState = new State
            {
                LogicState = !input[0].LogicState
            };
        }

        public override NodeBase Clone(string nodeId)
        {
            return new NotNode(nodeId)
            {
                CurrentState = this.CurrentState,
            };
        }
    }
}