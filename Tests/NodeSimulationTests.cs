using Library2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class NodeSimulationTests
    {
        [TestMethod]
        public void NotNodeTest()
        {
            var inputNode = new InputNode("in", new State(true));
            var notNode = new NotNode("not");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(inputNode, notNode),
                new NodeConnection(notNode, outputNode)
            };

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.IsFalse(output["out"].LogicState);
        }

        [TestMethod]
        public void NotNotNodeTest()
        {
            var inputNode = new InputNode("in", new State(true));
            var notNode1 = new NotNode("not1");
            var notNode2 = new NotNode("not2");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(inputNode, notNode1),
                new NodeConnection(notNode1, notNode2),
                new NodeConnection(notNode2, outputNode)
            };

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.IsTrue(output["out"].LogicState);
        }
    }
}
