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

        public void SetInputs(Dictionary<string, State> states)
        {
            var InputNodes = GetInputNodes();

            //if (states.Count != InputNodes.Count)
            //{
            //    throw new ArgumentException();
            //}
            foreach(KeyValuePair<string, State> state in states)
            {
                InputNodes.Where(x => x.NodeId == state.Key).Single().SetState(state.Value);
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

        // Check for loops in simulation, returns false for no loops
        public String ValidSimulationCheck()
        {
            // Determine output nodes
            var outputs = this.NodeConnections.Where(x => x.OutputNode is OutputNode).ToList();

            // Determine input nodes
            var inputs = GetInputNodes();

            // Check if Simulation contains output nodes
            if (outputs.Count == 0)
                return "Simulation contains no output nodes";

            // Check if Simulation contains input nodes
            if (inputs.Count == 0)
                return "Simulation contains no input nodes";

            // Create a list of paths
            List<List<NodeConnection>> pathList = new List<List<NodeConnection>>();

            // Add a path for every output
            foreach (var output in outputs)
            {
                pathList.Add(new List<NodeConnection> { output });
            }

            // Chech the paths for loops
            var remainingNodes = true;
            var loopsCheck = "";
            while (remainingNodes)
            {
                // Create a new temp list so the paths can be edited during the foreach loop
                var tempPathList = new List<List<NodeConnection>>(pathList);
                loopsCheck = ValidPathCheck(pathList, tempPathList);

                // If there are no more nodes left the while loop will stop
                if(loopsCheck != "Next nodes check") remainingNodes = false;
            }
            return loopsCheck;
        }

        // Check for loops in path, returns false for no loops
        private String ValidPathCheck(List<List<NodeConnection>> pathList, List<List<NodeConnection>> tempPathList)
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
                            return "Path does not end in input node";
                    }
                }

                foreach (var input in inputs)
                {
                    // If the node already exists in the current path a loop has been found
                    if (path.Contains(input)) return "Simulation contains loop(s)";
                    else
                    {
                        // Create a new path for every new node
                        pathList.Add(path);
                        pathList.Last().Add(input);
                    }
                }
                // Remove the obsolute path
                pathList.Remove(path);
            }

            // If any path still remains continue with next set of nodes
            if (pathList.Count() > 0) return "Next nodes check";

            // If no loops have been found return false
            return null;
        }
    }
}