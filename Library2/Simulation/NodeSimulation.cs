namespace DP1.Library.Simulation
{
    using DP1.Library.Nodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NodeSimulation
    {
        public NodeSimulation(List<NodeConnection> nodeConnections)
        {
            this.NodeConnections = nodeConnections;
        }

        public List<NodeConnection> NodeConnections { get; set; }

        public void RunSimulation()
        {
            // First determine output nodes
            var outputs = this.NodeConnections.Where(x => x.OutputNode is OutputNode).ToList();

            this.Calc(outputs);
        }

        private void Calc(List<NodeConnection> outputs)
        {
            var inputIds = outputs.SelectMany(x => x.InputNodes).Select(x => x.NodeId).ToList();

            var inputs = this.NodeConnections
                .Where(x => inputIds.Contains(x.OutputNode.NodeId)).ToList();

            if (inputs.Count != 0)
            {
                this.Calc(inputs);
            }

            foreach (var item in outputs)
            {
                item.GetResultFromOutputNode();
            }
        }

        public Dictionary<string, State> GetOutputState()
        {
            return this.NodeConnections
                .Where(x => x.OutputNode is OutputNode)
                .ToDictionary(x => x.OutputNode.NodeId, x => x.OutputNode.CurrentState);
        }
    }
}