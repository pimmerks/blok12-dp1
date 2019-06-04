namespace DP1.Library.Simulation
{
    using DP1.Library.Nodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NodeSimulation
    {
        public NodeSimulation(List<NodeConnection> nodeConnections, List<InputNode> inputNodes)
        {
            this.NodeConnections = nodeConnections;
            this.InputNodes = inputNodes;
        }

        public List<NodeConnection> NodeConnections { get; set; }
        public List<InputNode> InputNodes { get; set; }

        public void SetInputs(Dictionary<string, State> states)
        {
            if(states.Count != InputNodes.Count)
            {
                throw new ArgumentException();
            }
            foreach(KeyValuePair<string, State> state in states)
            {
                InputNodes.Where(x => x.NodeId == state.Key).Single().SetState(state.Value);
            }
        }

        public void ResetSimulation()
        {
            foreach(NodeConnection nodeConnection in this.NodeConnections)
            {
                nodeConnection.OutputNode.ResetState();
            }
        }

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