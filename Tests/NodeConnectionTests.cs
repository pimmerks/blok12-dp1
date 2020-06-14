namespace DP1.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Library;
    using Library.Nodes;
    using Library.Simulation;
    using Xunit;

    public class NodeConnectionTests
    {
        [Fact]
        public void NodeConnectionTest()
        {
            var inputNode = new InputNode("in", new State(true));
            var outputNode = new OutputNode("out");

            var nodeConnection = new NodeConnection(inputNode, outputNode);

            var state = nodeConnection.GetResultFromOutputNode();

            Assert.True(state.LogicState);
        }

        [Fact]
        public void NodeConnectionWithNotNodeTest()
        {
            var inputNode = new InputNode("in", new State(true));
            var notNode = new NotNode("not");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(inputNode, notNode),
                new NodeConnection(notNode, outputNode)
            };

            var outputs = nodeConnections.Where(x => x.OutputNode is OutputNode).ToList();

            var inputIds = outputs.SelectMany(x => x.InputNodes).Select(x => x.NodeId).ToList();

            var inputConnections = nodeConnections.Where(x => inputIds.Contains(x.OutputNode.NodeId));

            foreach (var input in inputConnections)
            {
                input.GetResultFromOutputNode();
            }

            //var t = nodeConnections.Where(x => x.InputNodes.Any(y => ))

            // Get which node connection has the not node as output
            // Do GetResultFromOutputNode on that node connection

            nodeConnections[1].GetResultFromOutputNode();

            Assert.False(nodeConnections[1].OutputNode.CurrentState.LogicState);
        }
    }
}
