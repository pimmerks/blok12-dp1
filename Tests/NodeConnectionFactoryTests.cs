using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DP1.Library.Factories;
using DP1.Library.Nodes;
using DP1.Library.Simulation;
using DP1.Library;
using DP1.Library.File;

namespace DP1.Tests
{
    [TestClass]
    public class NodeConnectionFactoryTests
    {
        NodeConnectionFactory nodeConnectionFactory;
        List<NodeBase> nodes;

        public NodeConnectionFactoryTests()
        {
            nodeConnectionFactory = new NodeConnectionFactory();
            nodes = new List<NodeBase>
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

        [TestMethod]
        public void CreateNodeConnectionTest()
        {
            var inputs = new List<string>
            {
                "IN2",
                "OR1"
            };
            List<NodeBase> inputNodes = nodes.Where(x => inputs.Contains(x.NodeId)).ToList();

            var nodeConnection = nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "AND1");


            Assert.IsInstanceOfType(nodeConnection, typeof(NodeConnection));
            CollectionAssert.AreEqual(inputNodes, nodeConnection.InputNodes);
            Assert.AreEqual(nodes.Where(x => x.NodeId == "AND1").Single(), nodeConnection.OutputNode);
        }

        [TestMethod]
        public void CreateUnrecognizedOutputTest()
        {
            var inputs = new List<string>
            {
                "IN2",
                "OR1"
            };
            Assert.ThrowsException<InvalidOperationException>(() => nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "UnrecognizedOutput"));
        }

        [TestMethod]
        public void CreateUnrecognizedInputTest()
        {
            var inputs = new List<string>
            {
                "AND2",
                "UnrecognizedInput"
            };
            Assert.ThrowsException<InvalidOperationException>(() => nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "OUT1"));
        }

        [TestMethod]
        public void CreateEmptyInputTest()
        {
            var inputs = new List<string>
            {
            };
            Assert.ThrowsException<ArgumentNullException>(() => nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "OUT1"));
        }

        [TestMethod]
        public void CreateEmptyNodesTest()
        {
            var inputs = new List<string>
            {
                "IN2",
                "OR1"
            };
            var emptyNodes = new List<NodeBase>();
            Assert.ThrowsException<InvalidOperationException>(() => nodeConnectionFactory.CreateNodeConnection(emptyNodes, inputs, "OUT1"));
        }

        [TestMethod]
        public void Convert1Test()
        {
            var defs = new List<NodeConnectionDefinition>
            {
                new NodeConnectionDefinition("IN1", new List<string> { "NOT1", "NOT2" }),
                new NodeConnectionDefinition("NOT1", new List<string> { "OUT1" }),
                new NodeConnectionDefinition("NOT2", new List<string> { "OUT2" }),
            };

            var nodeConnections = this.nodeConnectionFactory.Convert(this.nodes, defs);

            Assert.AreEqual(4, nodeConnections.Count);

            Assert.AreEqual("IN1", nodeConnections[0].InputNodes[0].NodeId);
            Assert.AreEqual("NOT1", nodeConnections[0].OutputNode.NodeId);

            Assert.AreEqual("IN1", nodeConnections[1].InputNodes[0].NodeId);
            Assert.AreEqual("NOT2", nodeConnections[1].OutputNode.NodeId);


            Assert.AreEqual("NOT1", nodeConnections[2].InputNodes[0].NodeId);
            Assert.AreEqual("OUT1", nodeConnections[2].OutputNode.NodeId);

            Assert.AreEqual("NOT2", nodeConnections[3].InputNodes[0].NodeId);
            Assert.AreEqual("OUT2", nodeConnections[3].OutputNode.NodeId);
        }

        [TestMethod]
        public void Convert2Test()
        {
            var defs = new List<NodeConnectionDefinition>
            {
                new NodeConnectionDefinition("IN1", new List<string> { "AND1" }),
                new NodeConnectionDefinition("IN2", new List<string> { "AND1" }),
                new NodeConnectionDefinition("AND1", new List<string> { "OUT1" }),
            };

            var nodeConnections = this.nodeConnectionFactory.Convert(this.nodes, defs);

            Assert.AreEqual(2, nodeConnections.Count);

            Assert.AreEqual("IN1", nodeConnections[0].InputNodes[0].NodeId);
            Assert.AreEqual("IN2", nodeConnections[0].InputNodes[1].NodeId);
            Assert.AreEqual("AND1", nodeConnections[0].OutputNode.NodeId);

            Assert.AreEqual("AND1", nodeConnections[1].InputNodes[0].NodeId);
            Assert.AreEqual("OUT1", nodeConnections[1].OutputNode.NodeId);
        }
    }
}
