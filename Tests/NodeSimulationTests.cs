using DP1.Library;
using DP1.Library.Nodes;
using DP1.Library.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System;

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

            var inputNodes = new List<InputNode>
            {
                inputNode
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

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

            var inputNodes = new List<InputNode>
            {
                inputNode
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.IsTrue(output["out"].LogicState);
        }

        [TestMethod]
        public void CreateSimulationTest()
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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

            CollectionAssert.AreEqual(inputNodes, simulation.InputNodes);
            CollectionAssert.AreEqual(nodeConnections, simulation.NodeConnections);
        }

        [TestMethod]
        public void ResetSimulationTest()
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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

            simulation.RunSimulation();
            simulation.ResetSimulation();

            var output = simulation.GetOutputState();
            Assert.IsNull(output["out"]);
        }

        [TestMethod]
        public void SetInputsTest()
        {
            var inputNode1 = new InputNode("in1");
            var inputNode2 = new InputNode("in2");
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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

            var inputValues = new Dictionary<string, State>
            {
                { inputNode1.NodeId, new State(true) },
                { inputNode2.NodeId, new State(false) }
            };

            simulation.SetInputs(inputValues);

            Assert.IsTrue(simulation.InputNodes.Where(x => x.NodeId == inputNode1.NodeId).Single().CurrentState.LogicState);
            Assert.IsFalse(simulation.InputNodes.Where(x => x.NodeId == inputNode2.NodeId).Single().CurrentState.LogicState);
        }

        [TestMethod]
        public void SetInvalidAmountInputsTest()
        {
            var inputNode1 = new InputNode("in1");
            var inputNode2 = new InputNode("in2");
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

            var inputNodes = new List<InputNode>
            {
                inputNode1,
                inputNode2
            };

            var simulation = new NodeSimulation(nodeConnections, inputNodes);

            var inputValues = new Dictionary<string, State>
            {
                { inputNode1.NodeId, new State(true) },
                { inputNode2.NodeId, new State(false) },
                { orNode.NodeId, new State(false) }
            };

            Assert.ThrowsException<ArgumentException>(() => simulation.SetInputs(inputValues));
        }
    }
}
