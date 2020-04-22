﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DP1.Library.Nodes
{
    public class OutputNode : NodeBase
    {
        /// <inheritdoc />
        public OutputNode(string nodeId)
            : base(nodeId)
        {
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
            if (!input.Any() || input.Length != 1)
            {
                throw new Exception($"An OutputNode can only contain 1 input. Id: {this.NodeId}");
            }

            this.CurrentState = new State(input[0].LogicState);
        }
    }
}