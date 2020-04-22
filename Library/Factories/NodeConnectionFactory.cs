namespace DP1.Library.Factories
{
    using File;
    using Nodes;
    using Simulation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NodeConnectionFactory
    {
        public NodeConnection CreateNodeConnection(List<NodeBase> nodes, List<string> inputs, string output)
        {
            if(inputs.Count <= 0)
            {
                throw new ArgumentException("A node connection requires at least 1 input", nameof(inputs));
            }

            var outputNode = nodes.Single(x => x.NodeId == output);
            
            var inputNodes = new List<NodeBase>();
            foreach(string input in inputs)
            {
                inputNodes.Add(nodes.Single(x => x.NodeId == input));
            }

            return new NodeConnection(inputNodes, outputNode);
        }

        /// <summary>
        /// Converts a list of <see cref="NodeBase"/> and <see cref="NodeConnectionDefinition"/>s to a list of
        /// <see cref="NodeConnection"/>s.
        /// </summary>
        /// <param name="nodes">The list of usable nodes.</param>
        /// <param name="definitions">The list of node definitions.</param>
        /// <returns>A list of node connections.</returns>
        public List<NodeConnection> Convert(List<NodeBase> nodes, List<NodeConnectionDefinition> definitions)
        {
            var outputs = definitions.SelectMany(x => x.OutputNodes).Distinct();

            var dict = new Dictionary<string, List<string>>();

            foreach (var ou in outputs)
            {
                var t = definitions
                    .Where(x => x.OutputNodes.Contains(ou));

                dict.Add(ou, t.Select(x => x.InputNode).ToList());
            }

            return dict.Select(x => this.CreateNodeConnection(nodes, x.Value, x.Key)).ToList();
        }
    }
}