namespace Library.Node.Nodes
{
    using System.Collections.Generic;

    public class NorNode : Node
    {
        /// <inheritdoc />
        public NorNode(string id, List<Output> inputs) : base(id, inputs)
        {
        }

        /// <inheritdoc />
        protected override void CalculateCurrentNode()
        {
            throw new System.NotImplementedException();
        }
    }
}