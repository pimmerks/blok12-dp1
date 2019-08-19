using System;
using System.Collections.Generic;
using DP1.Library;
using DP1.Library.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DP1.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NotNodeTest()
        {
            var notNode = new NotNode("");

            notNode.Calculate(new State(false));

            Assert.IsTrue(notNode.CurrentState.LogicState);
        }

        [TestMethod]
        public void InputNodeTest()
        {
            var inputNode = new InputNode("", new State(true));
            inputNode.Calculate();
            Assert.IsTrue(inputNode.CurrentState.LogicState);
        }

        [TestMethod]
        public void AndNodeTest()
        {
            var andNode = new AndNode("");

            var states = new List<State>
            {
                new State(true),
                new State(true)
            };

            andNode.Calculate(states.ToArray());

            Assert.IsTrue(andNode.CurrentState.LogicState);



            states = new List<State>
            {
                new State(false),
                new State(true)
            };

            andNode.Calculate(states.ToArray());

            Assert.IsFalse(andNode.CurrentState.LogicState);



            states = new List<State>
            {
                new State(false),
                new State(false)
            };

            andNode.Calculate(states.ToArray());

            Assert.IsFalse(andNode.CurrentState.LogicState);
        }

        [TestMethod]
        public void OrNodeTest()
        {
            var orNode = new OrNode("");

            var states = new List<State>
            {
                new State(true),
                new State(true)
            };

            orNode.Calculate(states.ToArray());

            Assert.IsTrue(orNode.CurrentState.LogicState);

            states = new List<State>
            {
                new State(false),
                new State(true)
            };

            orNode.Calculate(states.ToArray());

            Assert.IsTrue(orNode.CurrentState.LogicState);

            states = new List<State>
            {
                new State(false),
                new State(false)
            };

            orNode.Calculate(states.ToArray());

            Assert.IsFalse(orNode.CurrentState.LogicState);
        }

        [TestMethod]
        public void NorNodeTest()
        {
            var norNode = new NorNode("");

            var states = new List<State>
            {
                new State(true),
                new State(true)
            };

            norNode.Calculate(states.ToArray());

            Assert.IsFalse(norNode.CurrentState.LogicState);

            states = new List<State>
            {
                new State(false),
                new State(true)
            };

            norNode.Calculate(states.ToArray());

            Assert.IsFalse(norNode.CurrentState.LogicState);

            states = new List<State>
            {
                new State(false),
                new State(false)
            };

            norNode.Calculate(states.ToArray());

            Assert.IsTrue(norNode.CurrentState.LogicState);
        }

        [TestMethod]
        public void NandNodeTest()
        {
            var nandNode = new NandNode("");

            var states = new List<State>
            {
                new State(true),
                new State(true)
            };

            nandNode.Calculate(states.ToArray());

            Assert.IsFalse(nandNode.CurrentState.LogicState);



            states = new List<State>
            {
                new State(false),
                new State(true)
            };

            nandNode.Calculate(states.ToArray());

            Assert.IsTrue(nandNode.CurrentState.LogicState);



            states = new List<State>
            {
                new State(false),
                new State(false)
            };

            nandNode.Calculate(states.ToArray());

            Assert.IsTrue(nandNode.CurrentState.LogicState);
        }



        [TestMethod]
        public void XorNodeTest()
        {
            var xorNode = new XorNode("");

            var states = new List<State>
            {
                new State(true),
                new State(true)
            };

            xorNode.Calculate(states.ToArray());

            Assert.IsFalse(xorNode.CurrentState.LogicState);



            states = new List<State>
            {
                new State(false),
                new State(true)
            };

            xorNode.Calculate(states.ToArray());

            Assert.IsTrue(xorNode.CurrentState.LogicState);



            states = new List<State>
            {
                new State(false),
                new State(false)
            };

            xorNode.Calculate(states.ToArray());

            Assert.IsFalse(xorNode.CurrentState.LogicState);
        }
    }
}
