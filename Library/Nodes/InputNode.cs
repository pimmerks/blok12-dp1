namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public class InputNode : NodeBase
    {
        private readonly State initialState = null;

        public InputNode(string nodeId)
            : base(nodeId)
        {
        }

        public InputNode(string nodeId, State initialState)
            : base(nodeId)
        {
            this.initialState = initialState;
            this.CurrentState = initialState;
        }

        public void ResetInput()
        {
            this.SetState(this.initialState);
        }
        
        /// <inheritdoc />
        public override void Calculate(params State[] input)
        {
        }
        
        public override void Register(NodeFactory factory)
        {
            // Skip registration of the input node.
        }

        public override NodeBase Clone(string nodeId)
        {
            return new InputNode(nodeId, this.initialState)
            {
                CurrentState = this.CurrentState,
            };
        }

        public void SetState(State newState)
        {
            this.CurrentState = newState;
        }
    }
}