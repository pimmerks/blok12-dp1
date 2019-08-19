using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP1.Library.Nodes
{
    public class XorNode : NodeBase
    {
        private NandNode nandNode;
        private OrNode orNode;
        private AndNode andNode;

        /// <inheritdoc />
        public XorNode(string nodeId)
            : base(nodeId)
        {
            andNode = new AndNode(nodeId + "-And");
            nandNode = new NandNode(nodeId + "-Nand");
            orNode = new OrNode(nodeId + "-Or");
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length < 1)
            {
                throw new Exception("An XorNode must contain at least 2 inputs");
            }

            nandNode.Calculate(input);
            orNode.Calculate(input);
            andNode.Calculate(nandNode.CurrentState, orNode.CurrentState);

            this.CurrentState = new State
            {
                LogicState = andNode.CurrentState.LogicState
            };
        }
    }
}
