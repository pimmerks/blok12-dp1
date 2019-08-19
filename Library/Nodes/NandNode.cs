using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP1.Library.Nodes
{
    public class NandNode : NodeBase
    {
        private NotNode notNode;
        private AndNode andNode;

        /// <inheritdoc />
        public NandNode(string nodeId)
            : base(nodeId)
        {
            notNode = new NotNode(nodeId + "-Not");
            andNode = new AndNode(nodeId + "-And");
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length < 1)
            {
                throw new Exception("An NandNode must contain at least 2 inputs");
            }

            andNode.Calculate(input);
            notNode.Calculate(andNode.CurrentState);

            this.CurrentState = new State
            {
                LogicState = notNode.CurrentState.LogicState
            };
        }
    }
}
