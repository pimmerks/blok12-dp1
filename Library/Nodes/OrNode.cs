using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public class OrNode : NodeBase
    {
        /// <inheritdoc />
        public OrNode(string nodeId)
            : base(nodeId)
        {
        }

        public override void Register(NodeFactory factory)
        {
            factory.RegisterNode("OR", new OrNode(""));
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any())
            {
                throw new Exception($"An OrNode must contain at least 1 inputs. Id: {this.NodeId}");
            }

            
            this.CurrentState = new State
            {
                LogicState = input.Where(x => x.LogicState == true).Any()
            };
        }

        public override NodeBase Clone(string nodeId)
        {
            return new OrNode(nodeId)
            {
                CurrentState = this.CurrentState
            };
        }
    }
}