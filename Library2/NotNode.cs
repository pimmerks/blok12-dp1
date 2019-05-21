using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library2
{
    public class NotNode : NodeBase
    {
        /// <inheritdoc />
        public NotNode(string nodeId)
            : base(nodeId)
        {
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length != 1)
            {
                throw new Exception("A NotNode Can only contain 1 input");
            }

            this.CurrentState = new State
            {
                LogicState = !input[0].LogicState
            };
        }
    }
}