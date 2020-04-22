namespace DP1.Library.Simulation
{
    using System.Collections.Generic;

    public class NodeSimulationBuilder
    {
        private List<NodeConnection> nodeConnections = new List<NodeConnection>();

        private NodeSimulationBuilder()
        {
        }

        public static NodeSimulationBuilder GetBuilder()
        {
            return new NodeSimulationBuilder();
        }

        public NodeSimulationBuilder AddNodeConnections(List<NodeConnection> connections)
        {
            this.nodeConnections.AddRange(connections);
            return (NodeSimulationBuilder)this.MemberwiseClone();
        }

        public NodeSimulation Build()
        {
            return new NodeSimulation(this.nodeConnections);
        }
    }
}