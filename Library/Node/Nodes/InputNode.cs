namespace Library.Node.Nodes
{
    using System.Collections.Generic;
    using System.Linq;

    public class InputNode : Node
    {
        private State state;

        /// <inheritdoc />
        public InputNode(string id, List<Output> inputs, State initialState)
            : base(id, inputs)
        {
            this.state = initialState;
            this.Outputs = new List<Output>
            {
                new Output(this)
            };

        }

        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            this.Outputs.Single().SetState(this.state);
        }

        public void SetState(State newState)
        {
            this.state = newState;
        }
    }
}