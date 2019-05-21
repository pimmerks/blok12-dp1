using DP1.Library;
using DP1.Library.Nodes;
using DP1.Library.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DP1.Tests
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

        [TestMethod]
        public void AndNodeTest()
        {
            var inputNode1 = new InputNode("in1", new State(true));
            var inputNode2 = new InputNode("in2", new State(true));
            var andNode = new AndNode("and");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(
                    new List<NodeBase> {
                        inputNode1, inputNode2
                    },
                    andNode),
                new NodeConnection(andNode, outputNode)
            };

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.IsTrue(output["out"].LogicState);
        }

        [TestMethod]
        public void AndFalseNodeTest()
        {
            var inputNode1 = new InputNode("in1", new State(false));
            var inputNode2 = new InputNode("in2", new State(true));
            var andNode = new AndNode("and");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(
                    new List<NodeBase> {
                        inputNode1, inputNode2
                    },
                    andNode),
                new NodeConnection(andNode, outputNode)
            };

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.IsFalse(output["out"].LogicState);
        }

        [TestMethod]
        public void OrNodeTest()
        {
            var inputNode1 = new InputNode("in1", new State(false));
            var inputNode2 = new InputNode("in2", new State(true));
            var orNode = new OrNode("or");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(
                    new List<NodeBase> {
                        inputNode1, inputNode2
                    },
                    orNode),
                new NodeConnection(orNode, outputNode)
            };

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.IsTrue(output["out"].LogicState);
        }
    }
}
