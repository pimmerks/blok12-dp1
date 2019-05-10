namespace Library.Node.Nodes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NotNode : Node
    {
        /// <inheritdoc />
        public NotNode(string id, List<Output> inputs)
            : base(id, inputs)
        {
            if (inputs.Count != 1)
            {
                throw new Exception($"A NOT node can only contain 1 input.");
            }

            this.Outputs = new List<Output>
            {
                new Output(this)
            };
        }
        
        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            var s = this.Inputs.Single().GetState();

            this.Outputs.Single().SetState(State.Make(!s.LogicState));
        }
    }
}