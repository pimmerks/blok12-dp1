using System.Collections.Generic;

namespace Library2
{
    public class InputNode : NodeBase
    {
        private State currentState;

        /// <inheritdoc />
        public InputNode(string nodeId)
            : base(nodeId)
        {
        }

        /// <inheritdoc />
        public InputNode(string nodeId, State initialState)
            : base(nodeId)
        {
            this.currentState = initialState;
        }

        /// <inheritdoc />
        public override State Calculate(params State[] input)
        {
            return this.currentState;
        }

        public void SetState(State newState)
        {
            this.currentState = newState;
        }
    }
}