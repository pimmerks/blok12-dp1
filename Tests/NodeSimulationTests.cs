namespace DP1.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library;
    using Library.Nodes;
    using Library.Simulation;
    using Xunit;

    public class NodeSimulationTests
    {
        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.False(output["out"].LogicState);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.True(output["out"].LogicState);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.True(output["out"].LogicState);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.False(output["out"].LogicState);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();

            var output = simulation.GetOutputState();
            Assert.True(output["out"].LogicState);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);
            
            // TODO: Is this the same as CollectionAssert?
            Assert.Equal(nodeConnections, simulation.NodeConnections);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            simulation.RunSimulation();
            simulation.ResetSimulation();

            var output = simulation.GetOutputState();
            Assert.Null(output["out"]);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            var inputValues = new Dictionary<string, State>
            {
                { inputNode1.NodeId, new State(true) },
                { inputNode2.NodeId, new State(false) }
            };

            simulation.SetInputs(inputValues);

            Assert.True(simulation.GetInputNodes().Where(x => x.NodeId == inputNode1.NodeId).Single().CurrentState.LogicState);
            Assert.False(simulation.GetInputNodes().Where(x => x.NodeId == inputNode2.NodeId).Single().CurrentState.LogicState);
        }

        [Fact]
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

            var simulation = new NodeSimulation(nodeConnections);

            var inputValues = new Dictionary<string, State>
            {
                { inputNode1.NodeId, new State(true) },
                { inputNode2.NodeId, new State(false) },
                { orNode.NodeId, new State(false) }
            };

            Assert.Throws<ArgumentException>(() => simulation.SetInputs(inputValues));
        }

        [Fact]
        public void BuilderTest()
        {
            var builder = NodeSimulationBuilder.GetBuilder();
            Assert.Throws<ArgumentNullException>(() => builder.AddNodeConnections(null));
            var sim = builder.Build();
            
            Assert.NotNull(sim);
        }
    }
}
