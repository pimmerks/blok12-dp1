using Library2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class NodeConnectionTests
    {
        [TestMethod]
        public void NodeConnectionTest()
        {
            var inputNode = new InputNode("in", new State(true));
            var outputNode = new OutputNode("out");

            var nodeConnection = new NodeConnection(inputNode, outputNode);

            var state = nodeConnection.GetResultFromOutputNode();

            Assert.IsTrue(state.LogicState);
        }
    }
}
