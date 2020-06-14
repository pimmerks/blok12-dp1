namespace DP1.Tests
{
    using System.Collections.Generic;
    using Library;
    using Library.Nodes;
    using Library.Simulation;
    using Xunit;

    public class ValidSimulationCheckTest
    {
        [Fact]
        public void ValidCheckWithoutInputs()
        {
            var notNode1 = new NotNode("not1");
            var notNode2 = new NotNode("not2");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(notNode1, notNode2),
                new NodeConnection(notNode2, outputNode)
            };

            var simulation = new NodeSimulation(nodeConnections);

            Assert.Equal("Simulation contains no input nodes", simulation.ValidSimulationCheck());
        }

        [Fact]
        public void ValidCheckWithoutOutputs()
        {
            var inputNode = new InputNode("in", new State(true));
            var notNode1 = new NotNode("not1");
            var notNode2 = new NotNode("not2");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(inputNode, notNode1),
                new NodeConnection(notNode1, notNode2)
            };

            var inputNodes = new List<InputNode>
            {
                inputNode
            };

            var simulation = new NodeSimulation(nodeConnections);

            Assert.Equal("Simulation contains no output nodes",simulation.ValidSimulationCheck());
        }

        [Fact]
        public void ValidCheckWithInvalidPath()
        {
            var inputNode = new InputNode("in", new State(true));
            var notNode1 = new NotNode("not1");
            var notNode2 = new NotNode("not2");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(inputNode, notNode1),
                new NodeConnection(notNode2, outputNode)
            };

            var inputNodes = new List<InputNode>
            {
                inputNode
            };

            var simulation = new NodeSimulation(nodeConnections);

            Assert.Equal("Path does not end in input node", simulation.ValidSimulationCheck());
        }

        [Fact]
        public void ValidCheckWithLoops()
        {
            var inputNode1 = new InputNode("in1", new State(true));
            var notNode = new NotNode("not");
            var andNode = new AndNode("and");
            var outputNode = new OutputNode("out");

            var nodeConnections = new List<NodeConnection>
            {
                new NodeConnection(andNode, notNode),
                new NodeConnection(
                    new List<NodeBase> {
                        inputNode1, notNode
                    },
                    andNode),
                new NodeConnection(andNode, outputNode)
            };

            var inputNodes = new List<InputNode>
            {
                inputNode1
            };

            var simulation = new NodeSimulation(nodeConnections);
            
            Assert.Equal("Simulation contains loop(s)", simulation.ValidSimulationCheck());
        }

        [Fact]
        public void ValidCheckWithValidSimulation()
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

            Assert.Null(simulation.ValidSimulationCheck());
        }
    }
}
