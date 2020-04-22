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
            this.andNode = new AndNode(nodeId + "-And");
            this.nandNode = new NandNode(nodeId + "-Nand");
            this.orNode = new OrNode(nodeId + "-Or");
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length < 1)
            {
                throw new Exception($"An XorNode must contain at least 2 inputs. Id: {this.NodeId}");
            }

            this.nandNode.Calculate(input);
            this.orNode.Calculate(input);
            this.andNode.Calculate(this.nandNode.CurrentState, this.orNode.CurrentState);

            this.CurrentState = new State
            {
                LogicState = this.andNode.CurrentState.LogicState
            };
        }
    }
}
