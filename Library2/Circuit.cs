using System;
using System.Collections.Generic;
using System.Text;

namespace Library2
{
    public class Circuit : NodeBase
    {
        /// <inheritdoc />
        public Circuit(string nodeId) : base(nodeId)
        {
        }

        /// <inheritdoc />
        public override State Calculate(params State[] input)
        {
            throw new NotImplementedException();
        }
    }
}