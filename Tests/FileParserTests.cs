namespace DP1.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using Library.Exceptions;
    using Library.File;
    using Xunit;

    public class FileParserTests
    {
        [Fact]
        public void ParseNodeDefinitionLineTest()
        {
            var line = "IN: INPUT_HIGH; # test";
            var fp = new FileParser();
            var output = fp.ParseLine(line);

            Assert.IsType<NodeDefinition>(output);

            var o = output as NodeDefinition;

            Assert.NotNull(o);

            Assert.Equal("IN", o.NodeId);
            Assert.Equal("INPUT_HIGH", o.NodeType);
        }

        [Fact]
        public void ParseNodeConnectionDefinitionLineTest()
        {
            var line = "NODE1: NODE2,NODE3; # test";
            var fp = new FileParser();
            var output = fp.ParseLine(line);

            Assert.IsType<NodeConnectionDefinition>(output);

            var o = output as NodeConnectionDefinition;
            
            Assert.Equal("NODE1", o.InputNode);
            Assert.Equal("NODE2", o.OutputNodes[0]);
            Assert.Equal("NODE3", o.OutputNodes[1]);
        }
        
        [Fact]
        public void ReadFileLinesFileNotFoundTest()
        {
            var file = "";
            var fp = new FileParser();
            Assert.Throws<FileNotFoundException>(() => fp.ReadFileLines(file));
        }
        
        [Fact]
        public void ReadFileLinesTest()
        {
            var file = Path.Join(Environment.CurrentDirectory, "test-circuit1.txt");
            var fp = new FileParser();
            var lines = fp.ReadFileLines(file);
            Assert.Equal(42, lines.Count);
        }

        [Fact]
        public void ParseIncorrectLine1Test()
        {
            var line = "NODE1: NODE2,NODE3 # test";
            var fp = new FileParser();
            Assert.Throws<LineParseException>(() => fp.ParseLine(line));
        }

        [Fact]
        public void ParseIncorrectLine2Test()
        {
            var line = "NP1: INPUT_F";
            var fp = new FileParser();
            Assert.Throws<LineParseException>(() => fp.ParseLine(line));
        }

        [Fact]
        public void ParseIncorrectLine3Test()
        {
            var line = "NODE1:; # test";
            var fp = new FileParser();
            Assert.Throws<LineParseException>(() => fp.ParseLine(line));
        }

        [Fact]
        public void ParseIncorrectLine4Test()
        {
            var line = "NODE1:TEST,; # test";
            var fp = new FileParser();
            Assert.Throws<LineParseException>(() => fp.ParseLine(line));
        }

        [Fact]
        public void ParseIncorrectLine5Test()
        {
            var line = "NODE1:TEST:TEST; # test";
            var fp = new FileParser();
            Assert.Throws<LineParseException>(() => fp.ParseLine(line));
        }

        [Fact]
        public void ParseDuplicateNodeTest()
        {
            var line = "NODE1:TEST,TEST; # test";
            var fp = new FileParser();
            Assert.Throws<DuplicateNodeException>(() => fp.ParseLine(line));
        }

        [Fact]
        public void SmallInputFileTest()
        {
            var lines = FileParserTestInputData.SmallInput.Split(Environment.NewLine.ToCharArray());
            var fp = new FileParser();
            var o = fp.ParseLines(lines.ToList());

            Assert.Equal(3, o.nodeDefinitions.Count);
            Assert.Equal(2, o.nodeConnectionDefinitions.Count);
        }

        [Fact]
        public void MainInputFileTest()
        {
            var lines = FileParserTestInputData.MainInputFile.Split(Environment.NewLine.ToCharArray());
            var fp = new FileParser();
            var o = fp.ParseLines(lines.ToList());

            Assert.Equal(16, o.nodeDefinitions.Count);
            Assert.Equal(14, o.nodeConnectionDefinitions.Count);
        }
    }
}
