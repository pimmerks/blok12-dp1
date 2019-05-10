namespace Library.Node.Nodes
{
    using System.Collections.Generic;

    public class OutputNode : Node
    {
        /// <inheritdoc />
        public OutputNode(string id, List<Output> inputs)
            : base(id, inputs)
        {
        }

        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            throw new System.NotImplementedException();
        }
    }
}