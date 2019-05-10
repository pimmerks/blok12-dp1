namespace Library.Circuit
{
    using System.Collections.Generic;
    using Node;

    public class Circuit : Node
    {
        public List<Node> InputNodes { get; set; }

        public List<Node> OutputNodes { get; set; }

        /// <inheritdoc />
        public Circuit(string id, List<Output> inputs) : base(id, inputs)
        {
        }

        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            throw new System.NotImplementedException();
        }
    }
}