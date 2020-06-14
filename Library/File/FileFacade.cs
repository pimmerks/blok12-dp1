namespace DP1.Library.File
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DP1.Library.Factories;
    using DP1.Library.Simulation;

    public class FileFacade
    {
        public static List<NodeConnection> GetNodeConnectionsFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.", path);
            }

            var fp = new FileParser();
            var lines = fp.ReadFileLines(path);
            var (nodeDefinitions, nodeConnectionDefinitions) = fp.ParseLines(lines);

            var nodeFactory = NodeFactory.Instance;
            var nodeConFactory = new NodeConnectionFactory();

            // Convert definitions to nodes
            var nodes = nodeDefinitions.Select(nodeFactory.CreateNode).ToList();

            // And node connections
            return nodeConFactory.Convert(nodes, nodeConnectionDefinitions);
        }
    }
}
