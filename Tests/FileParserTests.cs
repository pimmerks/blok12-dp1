using DP1.Library.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DP1.Tests
{
    [TestClass]
    public class FileParserTests
    {
        [TestMethod]
        public void ParseNodeDefinitionLineTest()
        {
            var line = "IN: INPUT_HIGH; # test";
            var fp = new FileParser();
            var output = fp.ParseLine(line);

            Assert.IsInstanceOfType(output, typeof(NodeDefinition));

            var o = output as NodeDefinition;

            Assert.AreEqual("IN", o.NodeId);
            Assert.AreEqual("INPUT_HIGH", o.NodeType);
        }

        [TestMethod]
        public void ParseNodeConnectionDefinitionLineTest()
        {
            var line = "NODE1: NODE2,NODE3; # test";
            var fp = new FileParser();
            var output = fp.ParseLine(line);

            Assert.IsInstanceOfType(output, typeof(NodeConnectionDefinition));

            var o = output as NodeConnectionDefinition;

            Assert.AreEqual("NODE1", o.InputNode);
            Assert.AreEqual("NODE2", o.OutputNodes[0]);
            Assert.AreEqual("NODE3", o.OutputNodes[1]);
        }

        [TestMethod]
        public void ParseIncorrectLine1Test()
        {
            var line = "NODE1: NODE2,NODE3 # test";
            var fp = new FileParser();
            Assert.ThrowsException<Exception>(() => fp.ParseLine(line));
        }

        [TestMethod]
        public void ParseIncorrectLine2Test()
        {
            var line = "NP1: INPUT_F";
            var fp = new FileParser();
            Assert.ThrowsException<Exception>(() => fp.ParseLine(line));
        }

        [TestMethod]
        public void ParseIncorrectLine3Test()
        {
            var line = "NODE1:; # test";
            var fp = new FileParser();
            Assert.ThrowsException<Exception>(() => fp.ParseLine(line));
        }

        [TestMethod]
        public void SmallInputFileTest()
        {
            var lines = FileParserInput.SmallInput.Split(Environment.NewLine.ToCharArray());
            var fp = new FileParser();
            var o = fp.ParseLines(lines.ToList());

            Assert.AreEqual(3, o.nodeDefinitions.Count);
            Assert.AreEqual(2, o.nodeConnectionDefinitions.Count);
        }

        [TestMethod]
        public void MainInputFileTest()
        {
            var lines = FileParserInput.MainInputFile.Split(Environment.NewLine.ToCharArray());
            var fp = new FileParser();
            var o = fp.ParseLines(lines.ToList());

            Assert.AreEqual(16, o.nodeDefinitions.Count);
            Assert.AreEqual(14, o.nodeConnectionDefinitions.Count);
        }
    }

    class FileParserInput
    {

        public const string SmallInput = @"
# Nodes:
IN: INPUT_HIGH;
NOTNODE: NOT;
OUT: PROBE;

# EDGES:
IN: NOTNODE;
NOTNODE: OUT;";

        public const string MainInputFile = @"
# Circuit1.txt
#
# Full-adder. Deze file bevat een correct circuit
#
#
#
# Description of all the nodes
#
A: INPUT_HIGH;
B: INPUT_HIGH;
Cin: INPUT_LOW;
Cout: PROBE;
S: PROBE;
NODE1: OR;
NODE2: AND;
NODE3: AND;
NODE4: NOT;
NODE5: AND;
NODE6: OR;
NODE7: NOT;
NODE8: NOT;
NODE9: AND;
NODE10: AND;
NODE11: OR;
#
#
# Description of all the edges
#
Cin: NODE3,NODE7,NODE10;
A: NODE1,NODE2;
B: NODE1,NODE2;
NODE1: NODE3,NODE5;
NODE2: NODE4,NODE6;
NODE3: NODE6;
NODE4: NODE5;
NODE5: NODE8,NODE9;
NODE6: Cout;
NODE7: NODE9;
NODE8: NODE10;
NODE9: NODE11;
NODE10: NODE11;
NODE11: S;";
    }
}
