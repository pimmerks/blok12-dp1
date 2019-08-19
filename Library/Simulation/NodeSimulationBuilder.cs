namespace DP1.Library.Simulation
{
    using System.Collections.Generic;

    public class NodeSimulationBuilder
    {
        private List<NodeConnection> nodeConnections = new List<NodeConnection>();

        public static NodeSimulationBuilder GetBuilder()
        {
            return new NodeSimulationBuilder();
        }

        public NodeSimulationBuilder AddNodeConnections(List<NodeConnection> nodeConnections)
        {
            this.nodeConnections.AddRange(nodeConnections);
            return (NodeSimulationBuilder)this.MemberwiseClone();
        }

        public NodeSimulation Build()
        {
            return new NodeSimulation(this.nodeConnections);
        }
    }
}