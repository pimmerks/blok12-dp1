namespace DP1.Library.Factories
{
    using DP1.Library.File;
    using DP1.Library.Nodes;
    using DP1.Library.Simulation;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NodeConnectionFactory
    {
        public NodeConnection CreateNodeConnection(
            List<NodeBase> nodes, List<string> inputs, string output)
        {
            if(inputs.Count <= 0)
            {
                throw new ArgumentNullException("A node connection requires at least 1 input");
            }

            var outputNode = nodes.Where(x => x.NodeId == output).Single();
            
            var inputNodes = new List<NodeBase>();
            foreach(string input in inputs)
            {
                inputNodes.Add(nodes.Where(x => x.NodeId == input).Single());
            }

            return new NodeConnection(inputNodes, outputNode);
        }

        public List<NodeConnection> Convert(
            List<NodeBase> nodes, List<NodeConnectionDefinition> definitions)
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