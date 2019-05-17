﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Library2
{
    public class OutputNode : NodeBase
    {
        /// <inheritdoc />
        public OutputNode(string nodeId)
            : base(nodeId)
        {
        }

        /// <inheritdoc />
        public override State Calculate(params State[] input)
        {
            if (!input.Any() || input.Length != 1)
            {
                throw new Exception("An OutputNode can only contain 1 input");
            }

            return new State(input[0].LogicState);
        }
    }
}