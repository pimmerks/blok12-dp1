﻿using System;
using System.Collections.Generic;
using DP1.Library;
using DP1.Library.Nodes;
using DP1.Library.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class ValidSimulationCheckTest
    {
        [TestMethod]
        public void ValidCheckwithoutinputs()
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

            Assert.AreEqual(simulation.ValidSimulationCheck(), "Simulation contains no input nodes");
        }

        [TestMethod]
        public void ValidCheckwithoutoutputs()
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

            Assert.AreEqual(simulation.ValidSimulationCheck(), "Simulation contains no output nodes");
        }

        [TestMethod]
        public void ValidCheckwithInvalidPath()
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

            Assert.AreEqual(simulation.ValidSimulationCheck(), "Path does not end in input node");
        }

        [TestMethod]
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
            
            Assert.AreEqual(simulation.ValidSimulationCheck(), "Simulation contains loop(s)");
        }

        [TestMethod]
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

            Assert.IsNull(simulation.ValidSimulationCheck());
        }
    }
}