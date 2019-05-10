namespace Library.Node.Nodes
{
    using System.Collections.Generic;

    public class AndNode : Node
    {
        /// <inheritdoc />
        public AndNode(string id, List<Output> inputs) : base(id, inputs)
        {
        }

        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            throw new System.NotImplementedException();
        }
    }
}