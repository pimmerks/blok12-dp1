using System;
using System.Collections.Generic;
using Library2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NotNodeTest()
        {
            var notNode = new NotNode("");

            var newState = notNode.Calculate(new State(false));

            Assert.IsTrue(newState.LogicState);
        }

        [TestMethod]
        public void InputNodeTest()
        {
            var inputNode = new InputNode("", new State(true));

            Assert.IsTrue(inputNode.Calculate().LogicState);
        }
    }
}
