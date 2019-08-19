using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP1.Library.Nodes
{
    public class NorNode : NodeBase
    {
        private NotNode notNode;
        private OrNode orNode;

        /// <inheritdoc />
        public NorNode(string nodeId)
            : base(nodeId)
        {
            notNode = new NotNode(nodeId + "-Not");
            orNode = new OrNode(nodeId + "-Or");
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length < 2)
            {
                throw new Exception("An NorNode must contain at least 2 inputs");
            }

            orNode.Calculate(input);
            notNode.Calculate(orNode.CurrentState);

            this.CurrentState = new State
            {
                LogicState = notNode.CurrentState.LogicState
            };
        }
    }
}
