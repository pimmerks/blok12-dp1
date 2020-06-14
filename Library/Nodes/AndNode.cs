using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public class AndNode : NodeBase
    {
        /// <inheritdoc />
        public AndNode(string nodeId)
            : base(nodeId)
        {
        }

        public override void Register(NodeFactory factory)
        {
            factory.RegisterNode("AND", new AndNode(""));
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
                LogicState = input.All(x => x.LogicState != false)
            };
        }

        public override NodeBase Clone(string nodeId)
        {
            return new AndNode(nodeId);
        }
    }
}