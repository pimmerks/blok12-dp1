namespace DP1.Library.Factories
{
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
    }
}