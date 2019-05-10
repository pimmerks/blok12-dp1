namespace Library.Node.Nodes
{
    using System.Collections.Generic;

    public class OrNode : Node
    {
        /// <inheritdoc />
        public OrNode(string id, List<Output> inputs) : base(id, inputs)
        {
        }

        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            throw new System.NotImplementedException();
        }
    }
}