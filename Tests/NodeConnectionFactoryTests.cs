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
            var inputNodes = new List<NodeBase>
            {
                nodes.Where(x => x.NodeId == inputs[0]).Single(),
                nodes.Where(x => x.NodeId == inputs[1]).Single()
            };

            var nodeConnection = nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "AND1");


            Assert.IsInstanceOfType(nodeConnection, typeof(NodeConnection));
            Assert.AreEqual(inputNodes, nodeConnection.InputNodes);
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
            Assert.ThrowsException<ArgumentException>(() => nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "UnrecognizedOutput"));
        }

        [TestMethod]
        public void CreateUnrecognizedInputTest()
        {
            var inputs = new List<string>
            {
                "AND2",
                "UnrecognizedInput"
            };
            Assert.ThrowsException<ArgumentException>(() => nodeConnectionFactory.CreateNodeConnection(nodes, inputs, "OUT1"));
        }
    }
}
