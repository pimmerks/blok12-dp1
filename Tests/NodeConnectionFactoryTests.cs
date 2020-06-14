namespace DP1.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library;
    using Library.Factories;
    using Library.File;
    using Library.Nodes;
    using Library.Simulation;
    using Xunit;

    public class NodeConnectionFactoryTests
    {
        NodeConnectionFactory nodeConnectionFactory;
        List<NodeBase> nodes;

        public NodeConnectionFactoryTests()
        {
            this.nodeConnectionFactory = new NodeConnectionFactory();
            this.nodes = new List<NodeBase>
            {
                new InputNode("IN1", new State(true)),
                new InputNode("IN2", new State(false)),
                new OrNode("OR1"),
                new OrNode("OR2"),
                new OrNode("OR3"),
                new AndNode("AND1"),
                new AndNode("AND2"),
                new NotNode("NOT1"),
                new NotNode("NOT2"),
                new OutputNode("OUT1"),
                new OutputNode("OUT2")
            };
        }

        [Fact]
        public void CreateNodeConnectionTest()
        {
            var inputs = new List<string>
            {
                "IN2",
                "OR1"
            };
            List<NodeBase> inputNodes = this.nodes.Where(x => inputs.Contains(x.NodeId)).ToList();

            var nodeConnection = this.nodeConnectionFactory.CreateNodeConnection(this.nodes, inputs, "AND1");


            Assert.IsType<NodeConnection>(nodeConnection);
            // TODO: Is this the same as CollectionAssert?
            Assert.Equal(inputNodes, nodeConnection.InputNodes);
            Assert.Equal(this.nodes.Where(x => x.NodeId == "AND1").Single(), nodeConnection.OutputNode);
        }

        [Fact]
        public void CreateUnrecognizedOutputTest()
        {
            var inputs = new List<string>
            {
                "IN2",
                "OR1"
            };
            Assert.Throws<InvalidOperationException>(() => this.nodeConnectionFactory.CreateNodeConnection(this.nodes, inputs, "UnrecognizedOutput"));
        }

        [Fact]
        public void CreateUnrecognizedInputTest()
        {
            var inputs = new List<string>
            {
                "AND2",
                "UnrecognizedInput"
            };
            Assert.Throws<InvalidOperationException>(() => this.nodeConnectionFactory.CreateNodeConnection(this.nodes, inputs, "OUT1"));
        }

        [Fact]
        public void CreateEmptyInputTest()
        {
            var inputs = new List<string>
            {
            };
            Assert.Throws<ArgumentException>(() => this.nodeConnectionFactory.CreateNodeConnection(this.nodes, inputs, "OUT1"));
        }

        [Fact]
        public void CreateEmptyNodesTest()
        {
            var inputs = new List<string>
            {
                "IN2",
                "OR1"
            };
            var emptyNodes = new List<NodeBase>();
            Assert.Throws<InvalidOperationException>(() => this.nodeConnectionFactory.CreateNodeConnection(emptyNodes, inputs, "OUT1"));
        }

        [Fact]
        public void Convert1Test()
        {
            var defs = new List<NodeConnectionDefinition>
            {
                new NodeConnectionDefinition("IN1", new List<string> { "NOT1", "NOT2" }),
                new NodeConnectionDefinition("NOT1", new List<string> { "OUT1" }),
                new NodeConnectionDefinition("NOT2", new List<string> { "OUT2" }),
            };

            var nodeConnections = this.nodeConnectionFactory.Convert(this.nodes, defs);

            Assert.Equal(4, nodeConnections.Count);

            Assert.Equal("IN1", nodeConnections[0].InputNodes[0].NodeId);
            Assert.Equal("NOT1", nodeConnections[0].OutputNode.NodeId);

            Assert.Equal("IN1", nodeConnections[1].InputNodes[0].NodeId);
            Assert.Equal("NOT2", nodeConnections[1].OutputNode.NodeId);


            Assert.Equal("NOT1", nodeConnections[2].InputNodes[0].NodeId);
            Assert.Equal("OUT1", nodeConnections[2].OutputNode.NodeId);

            Assert.Equal("NOT2", nodeConnections[3].InputNodes[0].NodeId);
            Assert.Equal("OUT2", nodeConnections[3].OutputNode.NodeId);
        }

        [Fact]
        public void Convert2Test()
        {
            var defs = new List<NodeConnectionDefinition>
            {
                new NodeConnectionDefinition("IN1", new List<string> { "AND1" }),
                new NodeConnectionDefinition("IN2", new List<string> { "AND1" }),
                new NodeConnectionDefinition("AND1", new List<string> { "OUT1" }),
            };

            var nodeConnections = this.nodeConnectionFactory.Convert(this.nodes, defs);

            Assert.Equal(2, nodeConnections.Count);

            Assert.Equal("IN1", nodeConnections[0].InputNodes[0].NodeId);
            Assert.Equal("IN2", nodeConnections[0].InputNodes[1].NodeId);
            Assert.Equal("AND1", nodeConnections[0].OutputNode.NodeId);

            Assert.Equal("AND1", nodeConnections[1].InputNodes[0].NodeId);
            Assert.Equal("OUT1", nodeConnections[1].OutputNode.NodeId);
        }
    }
}
