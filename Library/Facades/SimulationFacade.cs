namespace DP1.Library.Facades
{
    using DP1.Library.Factories;
    using DP1.Library.File;
    using DP1.Library.Simulation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SimulationFacade
    {
        /// <summary>
        /// Loads a simulation from a file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns></returns>
        public NodeSimulation LoadSimulation(string path)
        {
            var fp = new FileParser();
            var lines = fp.ReadFileLines(path);
            var (nodeDefinitions, nodeConnectionDefinitions) = fp.ParseLines(lines);

            var nodeFactory = new NodeFactory();
            var nodeConFactory = new NodeConnectionFactory();

            // Convert definitions to nodes
            var nodes = nodeDefinitions.Select(nodeFactory.CreateNode).ToList();
            
            // And node connections
            var nodeConnections =
                nodeConFactory.Convert(nodes, nodeConnectionDefinitions);

            var nodeSimulation = new NodeSimulation(nodeConnections);

            return new NodeSimulation(nodeConnections);
        }
    }
}
