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
        public void ParseIncorrectLine4Test()
        {
            var line = "NODE1:TEST,; # test";
            var fp = new FileParser();
            Assert.ThrowsException<Exception>(() => fp.ParseLine(line));
        }

        [TestMethod]
        public void ParseIncorrectLine5Test()
        {
            var line = "NODE1:TEST:TEST; # test";
            var fp = new FileParser();
            Assert.ThrowsException<Exception>(() => fp.ParseLine(line));
        }

        [TestMethod]
        public void ParseDuplicateNodeTest()
        {
            var line = "NODE1:TEST,TEST; # test";
            var fp = new FileParser();
            Assert.ThrowsException<Exception>(() => fp.ParseLine(line));
        }

        [TestMethod]
        public void SmallInputFileTest()
        {
            var lines = FileParserTestInputData.SmallInput.Split(Environment.NewLine.ToCharArray());
            var fp = new FileParser();
            var o = fp.ParseLines(lines.ToList());

            Assert.AreEqual(3, o.nodeDefinitions.Count);
            Assert.AreEqual(2, o.nodeConnectionDefinitions.Count);
        }

        [TestMethod]
        public void MainInputFileTest()
        {
            var lines = FileParserTestInputData.MainInputFile.Split(Environment.NewLine.ToCharArray());
            var fp = new FileParser();
            var o = fp.ParseLines(lines.ToList());

            Assert.AreEqual(16, o.nodeDefinitions.Count);
            Assert.AreEqual(14, o.nodeConnectionDefinitions.Count);
        }
    }
}
