using System.Collections.Generic;

namespace Library2
{
    public class InputNode : NodeBase
    {
        public InputNode(string nodeId)
            : base(nodeId)
        {
        }

        public InputNode(string nodeId, State initialState)
            : base(nodeId)
        {
            this.CurrentState = initialState;
        }

        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
        }

        public void SetState(State newState)
        {
            this.CurrentState = newState;
        }
    }
}