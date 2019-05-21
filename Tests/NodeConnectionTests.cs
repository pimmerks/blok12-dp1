using Library2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class NodeConnectionTests
    {
        [TestMethod]
        public void NodeConnectionTest()
        {
            var inputNode = new InputNode("in", new State(true));
            var outputNode = new OutputNode("out");

            var nodeConnection = new NodeConnection(inputNode, outputNode);

            var state = nodeConnection.GetResultFromOutputNode();

            Assert.IsTrue(state.LogicState);
        }

        [TestMethod]
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

            Assert.IsFalse(nodeConnections[1].OutputNode.CurrentState.LogicState);
        }
    }
}
