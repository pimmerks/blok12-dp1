using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP1.Library.Factories;
using DP1.Library.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DP1.Tests
{
    [TestClass]
    public class NodeFactoryTests
    {
        NodeFactory nodeFactory;
        public NodeFactoryTests()
        {
            nodeFactory = new NodeFactory();
    }

        [TestMethod]
        public void CreateInputNodeTest()
        {
            var inputNodeLow = nodeFactory.CreateNode("Input_Low", "INPUT_LOW");
            var inputNodeHigh = nodeFactory.CreateNode("Input_High", "INPUT_HIGH");

            Assert.IsFalse(inputNodeLow.CurrentState.LogicState);
            Assert.IsTrue(inputNodeHigh.CurrentState.LogicState);
            Assert.AreEqual("Input_Low", inputNodeLow.NodeId);
            Assert.AreEqual("Input_High", inputNodeHigh.NodeId);
            Assert.IsInstanceOfType(inputNodeLow, typeof(InputNode));
            Assert.IsInstanceOfType(inputNodeHigh, typeof(InputNode));
        }

        [TestMethod]
        public void CreateOutputNodeTest()
        {
            var outputNode = nodeFactory.CreateNode("output", "PROBE");

            Assert.AreEqual("output", outputNode.NodeId);
            Assert.IsInstanceOfType(outputNode, typeof(OutputNode));
        }

        [TestMethod]
        public void CreateOrNodeTest()
        {
            var orNode = nodeFactory.CreateNode("or", "OR");

            Assert.AreEqual("or", orNode.NodeId);
            Assert.IsInstanceOfType(orNode, typeof(OrNode));
        }

        [TestMethod]
        public void CreateAndNodeTest()
        {
            var andNode = nodeFactory.CreateNode("and", "AND");

            Assert.AreEqual("and", andNode.NodeId);
            Assert.IsInstanceOfType(andNode, typeof(AndNode));
        }

        [TestMethod]
        public void CreateNotNodeTest()
        {
            var notNode = nodeFactory.CreateNode("not", "NOT");

            Assert.AreEqual("not", notNode.NodeId);
            Assert.IsInstanceOfType(notNode, typeof(NotNode));
        }

        [TestMethod]
        public void CreateUnsupportedNodeTypeTest()
        {
            Assert.ThrowsException<ArgumentException>(() => nodeFactory.CreateNode("UnsupportedNode", ""));
        }

        [TestMethod]
        public void CreateNodeWithoutIdTest()
        {
            Assert.ThrowsException<ArgumentException>(() => nodeFactory.CreateNode("", "PROBE"));
        }
    }
}
