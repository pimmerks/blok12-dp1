using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public class NandNode : NodeBase
    {
        private NotNode notNode;
        private AndNode andNode;

        /// <inheritdoc />
        public NandNode(string nodeId)
            : base(nodeId)
        {
            this.notNode = new NotNode(nodeId + "-Not");
            this.andNode = new AndNode(nodeId + "-And");
        }

        public override void Register(NodeFactory factory)
        {
            factory.RegisterNode("NAND", new NandNode(""));
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length < 1)
            {
                throw new Exception($"An NandNode must contain at least 2 inputs. Id: {this.NodeId}");
            }

            this.andNode.Calculate(input);
            this.notNode.Calculate(this.andNode.CurrentState);

            this.CurrentState = new State
            {
                LogicState = this.notNode.CurrentState.LogicState
            };
        }

        public override NodeBase Clone(string nodeId)
        {
            return new NandNode(nodeId)
            {
                CurrentState = this.CurrentState,
            };
        }
    }
}
