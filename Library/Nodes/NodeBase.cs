namespace DP1.Library.Nodes
{
    using DP1.Library.Factories;

    public abstract class NodeBase : Interfaces.IClonableNode<NodeBase>
    {
        public NodeBase(string nodeId)
        {
            this.NodeId = nodeId;
        }

        public string NodeId { get; set; }

        public void ResetState()
        {
            this.CurrentState = null;
        }

        /// <summary>
        /// Registers this node in the supplied <see cref="NodeFactory"/>.
        /// </summary>
        /// <param name="factory"></param>
        public abstract void Register(NodeFactory factory);

        /// <summary>
        /// Calculates the output of this node and stores it in <see cref="CurrentState"/>.
        /// </summary>
        /// <param name="input">The node inputs.</param>
        public abstract void Calculate(params State[] input);

        /// <summary>
        /// Gets the current state.
        /// </summary>
        public State CurrentState { get; protected set; }
        
        public abstract NodeBase Clone(string newId);
    }
}