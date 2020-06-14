namespace DP1.Tests
{
    using System.Collections.Generic;
    using Library;
    using Library.Nodes;
    using Xunit;

    public class NodeCalculationTests
    {
        [Fact]
        public void NotNodeTest()
        {
            var notNode = new NotNode("");

            notNode.Calculate(new State(false));

            Assert.True(notNode.CurrentState.LogicState);
        }

        [Fact]
        public void InputNodeTest()
        {
            var inputNode = new InputNode("", new State(true));
            inputNode.Calculate();
            Assert.True(inputNode.CurrentState.LogicState);
            inputNode.SetState(new State(false));
            Assert.False(inputNode.CurrentState.LogicState);
            inputNode.ResetInput();
            Assert.True(inputNode.CurrentState.LogicState);
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, false)]
        [InlineData(false, false, false)]
        public void AndNodeTest(bool input1, bool input2, bool expectedOutput)
        {
            var node = new AndNode("");
            var states = new List<State>
            {
                new State(input1),
                new State(input2)
            };
            node.Calculate(states.ToArray());

            Assert.Equal(expectedOutput, node.CurrentState.LogicState);
        }
        
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        public void OrNodeTest(bool input1, bool input2, bool expectedOutput)
        {
            var node = new OrNode("");
            var states = new List<State>
            {
                new State(input1),
                new State(input2)
            };
            node.Calculate(states.ToArray());

            Assert.Equal(expectedOutput, node.CurrentState.LogicState);
        }
        
        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        public void NorNodeTest(bool input1, bool input2, bool expectedOutput)
        {
            var node = new NorNode("");
            var states = new List<State>
            {
                new State(input1),
                new State(input2)
            };
            node.Calculate(states.ToArray());

            Assert.Equal(expectedOutput, node.CurrentState.LogicState);
        }
        
        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, true)]
        [InlineData(false, false, true)]
        public void NandNodeTest(bool input1, bool input2, bool expectedOutput)
        {
            var node = new NandNode("");
            var states = new List<State>
            {
                new State(input1),
                new State(input2)
            };
            node.Calculate(states.ToArray());

            Assert.Equal(expectedOutput, node.CurrentState.LogicState);
        }
        
        [Theory]
        [InlineData(true, true, false)]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        public void XorNodeTest(bool input1, bool input2, bool expectedOutput)
        {
            var node = new XorNode("");
            var states = new List<State>
            {
                new State(input1),
                new State(input2)
            };
            node.Calculate(states.ToArray());

            Assert.Equal(expectedOutput, node.CurrentState.LogicState);
        }
    }
}
