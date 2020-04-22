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
            this.notNode = new NotNode(nodeId + "-Not");
            this.orNode = new OrNode(nodeId + "-Or");
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length < 2)
            {
                throw new Exception($"An NorNode must contain at least 2 inputs. Id: {this.NodeId}");
            }

            this.orNode.Calculate(input);
            this.notNode.Calculate(this.orNode.CurrentState);

            this.CurrentState = new State
            {
                LogicState = this.notNode.CurrentState.LogicState
            };
        }
    }
}
