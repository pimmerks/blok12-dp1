using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DP1.Library.Nodes
{
    public class AndNode : NodeBase
    {
        /// <inheritdoc />
        public AndNode(string nodeId)
            : base(nodeId)
        {
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any())
            {
                throw new Exception("An AndNode must contain at least 1 inputs. Id: {this.NodeId}");
            }


            this.CurrentState = new State
            {
                LogicState = !input.Any(x => x.LogicState == false)
            };
        }
    }
}