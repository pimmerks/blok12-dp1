namespace Library2
{
    using System.Collections.Generic;

    public abstract class NodeBase
    {
        public NodeBase(string nodeId)
        {
            this.NodeId = nodeId;
        }

        public string NodeId { get; set; }

        public abstract State Calculate(params State[] input);

        public State CurrentState { get; protected set; }
    }
}