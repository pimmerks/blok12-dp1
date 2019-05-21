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
            var definitions = fp.ParseLines(lines);

            // Convert definitions to simulation
            var nodeFactory = new NodeFactory();
            var nodes = definitions.nodeDefinitions
                .Select(nodeFactory.CreateNode);

            throw new NotImplementedException();
        }
    }
}
