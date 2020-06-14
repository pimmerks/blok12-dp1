namespace DP1.Library.Simulation
{
    using Nodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DP1.Library.Exceptions;

    public class NodeSimulation
    {
        public NodeSimulation(List<NodeConnection> nodeConnections)
        {
            this.NodeConnections = nodeConnections;
        }

        public List<NodeConnection> NodeConnections { get; set; }

        public double DelayTime { get; set; }

        public void SetInputs(Dictionary<string, State> states)
        {
            var inputNodes = this.GetInputNodes();

            if (states.Count > inputNodes.Count)
            {
                throw new ArgumentException();
            }

            foreach (var state in states)
            {
                inputNodes.Single(x => x.NodeId == state.Key).SetState(state.Value);
            }
        }

        public List<InputNode> GetInputNodes()
        {
             return this.NodeConnections
                .SelectMany(x =>
                    x.InputNodes.Where(y => y is InputNode))
                .Select(x => x as InputNode)
                .Distinct()
                .ToList();
        }

        public void ResetSimulation()
        {
            foreach(var nodeConnection in this.NodeConnections)
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

        // Check for loops in simulation, returns false for no loops
        public void ValidSimulationCheck()
        {
            // Set delay time to 0
            this.DelayTime = 0;

            // Determine output nodes
            var outputs = this.NodeConnections.Where(x => x.OutputNode is OutputNode).ToList();

            // Determine input nodes
            var inputs = this.GetInputNodes();

            // Check if Simulation contains output nodes
            if (outputs.Count == 0)
                throw new NoOutputNodesException("Simulation contains no output nodes");

            // Check if Simulation contains input nodes
            if (inputs.Count == 0)
                throw new NoInputNodesException("Simulation contains no input nodes");

            // Check for not connected
            foreach (var connection in this.NodeConnections)
            {
                if (!connection.InputNodes.Any())
                {
                    throw new InvalidConnectionException($"Connection {connection} has no input nodes.");
                }

                if (connection.OutputNode.GetType() != typeof(OutputNode) && connection.OutputNode == null)
                {
                    throw new InvalidConnectionException($"Connection {connection} has no output node.");
                }
            }

            // Create a list of paths
            var pathList = outputs.Select(output => new List<NodeConnection> { output }).ToList();

            // Add a path for every output

            // Check the paths for loops
            var remainingNodes = true;
            while (remainingNodes)
            {
                // Create a new temp list so the paths can be edited during the foreach loop
                var tempPathList = new List<List<NodeConnection>>(pathList);
                remainingNodes = this.ValidPathCheck(pathList, tempPathList);
            }
        }

        // Check for loops in path, returns false for no loops
        private bool ValidPathCheck(List<List<NodeConnection>> pathList, List<List<NodeConnection>> tempPathList)
        {
            foreach (var path in tempPathList)
            {
                var currentNode = path.Last();

                // Get the next set of nodes
                var inputIds = currentNode.InputNodes.Select(x => x.NodeId).ToList();
                var inputs = this.NodeConnections
                    .Where(x => inputIds.Contains(x.OutputNode.NodeId)).ToList();

                // Check if path ends in an input node
                if (inputs.Count == 0)
                {
                    foreach(var inputNode in currentNode.InputNodes)
                    {
                        if(inputNode.GetType() != typeof(InputNode))
                            throw new PathException("Path does not end in input node");
                    }
                }

                foreach (var input in inputs)
                {
                    // If the node already exists in the current path a loop has been found
                    if (path.Contains(input))
                    {
                        throw new PathException("Simulation contains loop(s)");
                        // return "Simulation contains loop(s)";
                    }
                    else
                    {
                        // Create a new path for every new node
                        pathList.Add(path);
                        pathList.Last().Add(input);
                    }
                }
                // Check if path is the longest path
                if (this.DelayTime < path.Count()) this.DelayTime = path.Count();

                // Remove the obsolete path
                pathList.Remove(path);
            }

            // If any path still remains continue with next set of nodes
            return pathList.Any();
        }
    }
}