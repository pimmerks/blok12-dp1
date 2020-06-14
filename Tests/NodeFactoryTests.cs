namespace DP1.Tests
{
    using System;
    using Library.Factories;
    using Library.File;
    using Library.Nodes;
    using Xunit;

    public class NodeFactoryTests
    {
        NodeFactory nodeFactory;
        public NodeFactoryTests()
        {
            this.nodeFactory = NodeFactory.Instance;
        }
        
        [Theory]
        [InlineData("PROBE", typeof(OutputNode))] // Do not test input nodes here...
        [InlineData("AND", typeof(AndNode))]
        [InlineData("OR", typeof(OrNode))]
        [InlineData("NOT", typeof(NotNode))]
        [InlineData("NAND", typeof(NandNode))]
        [InlineData("NOR", typeof(NorNode))]
        [InlineData("XOR", typeof(XorNode))]
        public void NodeFactoryShouldCreateCorrectNode(string nodeType, Type expectedType)
        {
            var node = this.nodeFactory.CreateNode(nodeType, nodeType);

            Assert.Equal(nodeType, node.NodeId);
            Assert.IsType(expectedType, node);
            Assert.Throws<Exception>(() => node.Calculate());
        }

        [Fact]
        public void CreateInputNodeTest()
        {
            var inputNodeLow = this.nodeFactory.CreateNode("Input_Low", "INPUT_LOW");
            var inputNodeHigh = this.nodeFactory.CreateNode("Input_High", "INPUT_HIGH");

            Assert.False(inputNodeLow.CurrentState.LogicState);
            Assert.True(inputNodeHigh.CurrentState.LogicState);
            Assert.Equal("Input_Low", inputNodeLow.NodeId);
            Assert.Equal("Input_High", inputNodeHigh.NodeId);
            Assert.IsType<InputNode>(inputNodeLow);
            Assert.IsType<InputNode>(inputNodeHigh);
        }

        [Fact]
        public void CreateUnsupportedNodeTypeTest()
        {
            Assert.Throws<ArgumentException>(() => this.nodeFactory.CreateNode("UnsupportedNode", ""));
        }

        [Fact]
        public void CreateNodeWithoutIdTest()
        {
            Assert.Throws<ArgumentException>(() => this.nodeFactory.CreateNode("", "PROBE"));
        }

        [Fact]
        public void CreateNodeFromDefinitionTest()
        {
            var def = new NodeDefinition("id", "OR");
            var node = this.nodeFactory.CreateNode(def);

            Assert.Equal("id", node.NodeId);
            Assert.IsType<OrNode>(node);
        }
    }
}
